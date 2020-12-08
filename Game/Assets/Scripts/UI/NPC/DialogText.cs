using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Class responsible for DialogText from NPCs
/// </summary>
public class DialogText : MonoBehaviour
{
    // Gameobject of the current text line in inspector
    [SerializeField] private GameObject currentLine;

    // Lines of text for the npc dialog
    [TextArea(1, 2)]
    [SerializeField] private string[] linesOfText;

    /// <summary>
    /// Property that returns linesOftext
    /// </summary>
    public string[] LinesOfText { get => linesOfText; }

    // Text with the prize text
    [TextArea(1, 2)]
    [SerializeField] private string prizeText;

    // Text when the npc opens the door
    [TextArea(1, 2)]
    [SerializeField] private string openDoorText;

    /// <summary>
    /// Property that checks if the NPC can give the prize
    /// </summary>
    public bool GivePrize {get; set;}

    /// <summary>
    /// Property that checks if the NPC can open the door
    /// </summary>
    public bool OpenDoor { get; set; }

    /// <summary>
    /// Property that defines how many seconds to wait
    /// </summary>
    public YieldInstruction WaitForSecs { get; set; }

    /// <summary>
    /// Counter to know how many times the player has spoken to the npc
    /// </summary>
    public byte Counter { get; set; }

    /// <summary>
    /// Coroutine responsible for getting next line of dialog in every lines
    /// </summary>
    /// <returns>Returns null</returns>
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
