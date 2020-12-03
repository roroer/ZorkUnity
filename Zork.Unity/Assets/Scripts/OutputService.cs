using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zork;
using UnityEngine.UI;

public class OutputService : MonoBehaviour, IOutputService

{
    [SerializeField]
    [Range(10, 1000)]
    private int MaxEntries = 50;

    [SerializeField]
    private Transform OutputTextContainer;

    [SerializeField]
    private TextMeshProUGUI TextLinePrefab;

    [SerializeField]
    private Image NewLinePrefab;

    public OutputService() {
        _entries = new List<GameObject>(MaxEntries);
    }

    public void WriteLine(string value, bool isBold = false) {
        var lines = value.Split(LineDelimiters, System.StringSplitOptions.None);

        foreach(var line in lines) {

            if(_entries.Count >= MaxEntries) {
                var entry = _entries.First();
                _entries.Remove(entry);
                Destroy(entry);
            }
            if (string.IsNullOrWhiteSpace(line)) {
                WriteNewLine();
            } else {
                WriteTextLine(line, isBold);
            }
        }
    }

    public void WriteLine(object value) => WriteLine(value.ToString());

    public void Write(string value) => WriteTextLine(value);

    public void Write(object value) => Write(value.ToString());

    private void WriteTextLine(string value, bool isBold = false) {
        var textLine = Instantiate(TextLinePrefab, OutputTextContainer);
        textLine.text = value;
        if (isBold) {
            textLine.fontStyle = TMPro.FontStyles.Bold;
        }
        _entries.Add(textLine.gameObject);
    }

    private void WriteNewLine() {
        var newLine = Instantiate(NewLinePrefab, OutputTextContainer);
        _entries.Add(newLine.gameObject);
    }

    static readonly string[] LineDelimiters = { "\n" };
    private readonly List<GameObject> _entries;
}
