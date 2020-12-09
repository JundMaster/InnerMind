using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used in Player's family pictures
/// </summary>
public class InteractionFamilyPicture : InteractionCommon
{
    //Components
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private string thought;

    //Variables in Inspector
    [SerializeField] private string[] thoughts;
    [SerializeField] private GameObject thoughtCanvas;

    /// <summary>
    /// Start method of InteractionFamilyPicture
    /// </summary>
    private void Start()
    {
        waitForSeconds = new WaitForSeconds(2);
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
    }


    /// <summary>
    /// This method determines the action of the picture when clicked
    /// </summary>
    public override void Execute()
    {
        StartCoroutine(DisplayThougthText(thoughts));
    }

    /// <summary>
    /// Renders thougths regarding the interacted object
    /// one at a time during a few seconds before dissapearing
    /// </summary>
    /// <param name="thoughts"> Array with all thoughts regarding the object</param>
    /// <returns>Returns paused time in seconds</returns>
    private IEnumerator DisplayThougthText(string[] thoughts)
    {

        for (int i = 0; i < thoughts.Length; i++)
        {
            thought = thoughts[i];
            thoughtCanvas.SetActive(true);
            displayText.enabled = true;
            displayText.text = thought;
            yield return waitForSeconds;
            thoughtCanvas.SetActive(false);
            displayText.enabled = false;
        }
    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "Examine Photo";
}
