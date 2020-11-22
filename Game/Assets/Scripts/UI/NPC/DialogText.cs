using System.Collections;
using UnityEngine;
using TMPro;

public class DialogText : MonoBehaviour
{
    [SerializeField] private GameObject currentLine;

    // Text lines in inspector
    [SerializeField] private string[] linesOfText;
    public string[] LinesOfText { get => linesOfText; }

    public byte Counter {get; set;}

    public YieldInstruction WaitForSecs { get; set; }

    // Sets active to call animation, sets the currentline, and turns false
    public IEnumerator GetNextLine()
    {
        // Sets current line equal to that line number on inspector
        currentLine.GetComponentInChildren<TextMeshProUGUI>().text =
                linesOfText[Counter];

        if (currentLine)
            currentLine.SetActive(true);
        yield return WaitForSecs;
        if (currentLine)
            currentLine.SetActive(false);
        yield return null;
    }
}
