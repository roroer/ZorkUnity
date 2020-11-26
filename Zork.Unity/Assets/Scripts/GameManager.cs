using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zork;
using Newtonsoft.Json;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string GameFilename = "Zork";

    [SerializeField]
    private OutputService Outputservice;
    [SerializeField]
    private InputService InputService;

    [SerializeField]
    private TextMeshProUGUI LocationText;
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    [SerializeField]
    private TextMeshProUGUI MoveText;

    private Game game;

    void Start()
    {
        TextAsset gameTextAsset = Resources.Load<TextAsset>(GameFilename);
        Debug.Log(gameTextAsset.text);

        game = JsonConvert.DeserializeObject<Game>(gameTextAsset.text);
        game.Initialize(InputService, Outputservice);

        game.Player.LocationChanged += (object sender, LocationChangedEventArgs e) => LocationText.text = e.NewLocation != null ? e.NewLocation.Name : "Unknkown";
        LocationText.text = game.Player.Location.Name;

        game.Player.MovesChanged += (object sender, int e) => MoveText.text = $"Moves: {e}";
        game.Player.ScoreChanged += (object sender, int e) => ScoreText.text = $"Score: {e}";

        InputService.InputField.ActivateInputField();
    }

    private void Player_LocationChanged(object sender, LocationChangedEventArgs e) {
        LocationText.text = e.NewLocation != null ? e.NewLocation.Name : "Location";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && string.IsNullOrWhiteSpace(InputService.InputField.text) == false) {
            InputService.ProcessInput();
            InputService.InputField.ActivateInputField();

            LocationText.text = game.Player.Location.Name;
        }

#if UNITY_EdITOR
        if (game.IsRunning == false) {
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
