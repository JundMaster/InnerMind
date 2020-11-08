using System.Collections;
using UnityEngine;
using TMPro;

public class NPCText : MonoBehaviour
{
    [SerializeField] private GameObject currentLine;

    // Text lines in inspector
    [SerializeField] private string[] linesOfText;

    public byte Counter {get; set;}

    // Sets active to call animation, sets the currentline, and turns false
    public IEnumerator NextLine()
    {
        // Sets current line equal to that line number on inspector
        currentLine.GetComponentInChildren<TextMeshProUGUI>().text =
                linesOfText[Counter];

        if (currentLine)
            currentLine.SetActive(true);
        yield return new WaitForSeconds(3f);
        if (currentLine)
            currentLine.SetActive(false);
        yield return null;
    }
}
