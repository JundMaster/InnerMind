﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used in Player's family pictures
/// </summary>
public class InteractionFamilyPicture : InteractionCommon, ICoroutineT<string[]>
{
    //Components
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private string thought;

    //Variables in Inspector
    [SerializeField] private string[] thoughts;
    [SerializeField] private Canvas thoughtCanvas;

    public Coroutine ThisCoroutine {get; private set;}

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
        if (ThisCoroutine == null)
            if (thoughtCanvas.enabled == false)
                ThisCoroutine = StartCoroutine(CoroutineExecute(thoughts));
    }

    /// <summary>
    /// Renders thougths regarding the interacted object
    /// one at a time during a few seconds before dissapearing
    /// </summary>
    /// <param name="thoughts"> Array with all thoughts regarding the object</param>
    /// <returns>Returns paused time in seconds</returns>
    public IEnumerator CoroutineExecute(string[] thoughts)
    {

        for (int i = 0; i < thoughts.Length; i++)
        {
            thought = thoughts[i];
            thoughtCanvas.enabled = true;
            displayText.enabled = true;
            displayText.text = thought;
            yield return waitForSeconds;
            thoughtCanvas.enabled = false;
            displayText.enabled = false;
        }
        ThisCoroutine = null;
    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "Examine Photo";

}
