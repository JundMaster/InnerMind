using System.Collections;
using UnityEngine;
using TMPro;

public class DialogText : MonoBehaviour
{
    // Text lines in inspector
    [SerializeField] private GameObject currentLine;
    [TextArea(1, 2)]
    [SerializeField] private string[] linesOfText;
    public string[] LinesOfText { get => linesOfText; }

    [TextArea(1, 2)]
    [SerializeField] private string prizeText;

    [TextArea(1, 2)]
    [SerializeField] private string openDoorText;

    public bool GivePrize {get; set;}

    public bool OpenDoor { get; set; }

    public YieldInstruction WaitForSecs { get; set; }

    public byte Counter { get; set; }

    // Sets active to call animation, sets the currentline, and turns false
    public IEnumerator GetNextLine()
    {
        if (GivePrize)
        {
            // Sets current line equal to text line on inspector
            currentLine.GetComponentInChildren<TextMeshProUGUI>().text =
                    prizeText;
        }
        if (OpenDoor)
        {
            // Sets current line equal to text line on inspector
            currentLine.GetComponentInChildren<TextMeshProUGUI>().text =
                    openDoorText;
        }
        else
        {
            // Sets current line equal to that line number on inspector
            currentLine.GetComponentInChildren<TextMeshProUGUI>().text =
                    linesOfText[Counter];
        }

        // Activates current line
        if (currentLine)
            currentLine.SetActive(true);

        // Waits for seconds
        yield return WaitForSecs;

        // Deactivates current line
        if (currentLine)
            currentLine.SetActive(false);

        yield return null;
    }
}
