using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zork;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string GameFilename = "Zork";

    [SerializeField]
    private OutputService Outputservice;
    [SerializeField]
    private InputService InputService;

    private Game game;

    void Start()
    {
        TextAsset gameTextAsset = Resources.Load<TextAsset>(GameFilename);
        Debug.Log(gameTextAsset.text);

        game = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);

        //game.Initialize(input, output);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EdITOR
        if (game.IsRunning == false) {
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
