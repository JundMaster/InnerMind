﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

/// <summary>
/// Class used when interacting with the piano with missing keys
/// </summary>
public class InteractionMissingPianoKey : InteractionCommon, ICoroutineT<string>
{
    //Components
    private BoxCollider boxCollider;
    private Inventory inventory;
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private StringBuilder thought;


    //Inspector Variables

    [SerializeField] private GameObject thoughtCanvas;
    [SerializeField] private GameObject keyPosition;
    [SerializeField] private ScriptableItem[] pianoKeys;

    ///<summary>
    ///Thought coroutine variable to control the coroutine
    /// </summary>
    public Coroutine ThisCoroutine { get; private set; }

    /// <summary>
    /// Property used to check if the player can interact with the piano,
    /// when entering the room
    /// </summary>
    public bool CanInteractWithPiano { get; private set; }


    /// <summary>
    /// Start method of InteractionMissingPianoKey
    /// </summary>
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        waitForSeconds = new WaitForSeconds(3);
        ThisCoroutine = null;
        thought = new StringBuilder(" ");
        

        CanInteractWithPiano = inventory.Bag.Contains(pianoKeys[0]) &&
                inventory.Bag.Contains(pianoKeys[1]) &&
                    inventory.Bag.Contains(pianoKeys[2]);
    }

    /// <summary>
    /// This method determines the action of the missing key when clicked
    /// </summary>
    public override void Execute()
    {
        if (CanInteractWithPiano)
        {
            for (int i = 0; i < pianoKeys.Length; i++)
            {

                //Checks if the player has piano keys in its inventory
                //placing missing key and removing it from it's inventory
                if (inventory.Bag.Contains(pianoKeys[i]))
                {

                    inventory.Bag.Remove(pianoKeys[i]);
                    keyPosition.SetActive(true);
                    boxCollider.enabled = false;
                    thought.Replace(" ", "Ah, there ya go.");
                    break;


                }
            }
        }
        else
        {
            thought.Replace(" ", "I should find the missing");
            thought.Append(" pieces, before checking if its tuned...");
        }

        if (ThisCoroutine == null && !thoughtCanvas.activeSelf)
        {
            ThisCoroutine = StartCoroutine(CoroutineExecute(thought.ToString()));
        }
        
    }


    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        string str = "Examine Piano";
        for (int i = 0; i < pianoKeys.Length; i++)
        {
            //If the player has a key in its inventory changes the returned
            //string
            if (CanInteractWithPiano)
            {
                str = "Place Missing Key";
            }
        }

        return str;
    }

    /// <summary>
    /// Renders a thought during a few seconds
    /// </summary>
    /// <param name="thought">Represents a single thought</param>
    /// <returns>Returns paused time in seconds</returns>
    public IEnumerator CoroutineExecute(string thought)
    {
        thoughtCanvas.SetActive(true);
        displayText.text = thought;
        displayText.enabled = true;
        yield return waitForSeconds;
        displayText.enabled = false;
        thoughtCanvas.SetActive(false);
        ThisCoroutine = null;
    }
}
