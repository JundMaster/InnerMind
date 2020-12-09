﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionMedicineCabinet : InteractionCommon
{
    //Components 
    private Animator cabinetDoorAnimation;
    private BoxCollider closetBoxCollider;
    private Inventory inventory;
    private Text displayText;
    private WaitForSeconds waitForSeconds;

    /// <summary>
    /// Property used to check if the player has opened the cabinet
    /// </summary>
    public bool IsOpen { get; private set; }

   //Inspector Variables
    [SerializeField] private string thought;
    [SerializeField] private GameObject thoughtCanvas;
    [SerializeField] private ScriptableItem cabinetKey;

    /// <summary>
    /// Start method of InteractionMedicineCabinet
    /// </summary>
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        IsOpen = false;
        cabinetDoorAnimation = GetComponentInChildren<Animator>();
        closetBoxCollider = GetComponent<BoxCollider>();
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        waitForSeconds = new WaitForSeconds(3);
    }

    /// <summary>
    /// This method determines the action of the cabinet when clicked
    /// </summary>
    public override void Execute()
    {
        //If the player has the key, removes it from inventory
        //and plays the animation
        if (inventory.Bag.Contains(cabinetKey))
        {
            thought = null;
            IsOpen = true;
            closetBoxCollider.enabled = false;
            cabinetDoorAnimation.SetTrigger("Open Door");
            inventory.Bag.Remove(cabinetKey);
        }
        //Else it displays a thought on the locked cabinet
        StartCoroutine(DisplayThougthText(thought));

    }





    /// <summary>
    /// Renders a thought during a few seconds
    /// </summary>
    /// <param name="thought">Represents a single thought</param>
    /// <returns>Returns paused time in seconds</returns>
    private IEnumerator DisplayThougthText(string thought)
    {
        //Checks if there is a thought to render
        if (thought != null)
        {
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
    public override string ToString() => "Open Cabinet";

}




