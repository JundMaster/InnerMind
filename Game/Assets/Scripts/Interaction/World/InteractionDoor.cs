﻿using UnityEngine;

/// <summary>
/// Class for every interactable door that the player can use
/// Extends InteractionCommon
/// </summary>
public class InteractionDoor : InteractionCommon
{
    /// <summary>
    /// This method determines the action of the door when clicked
    /// </summary>
    public override void Execute()
    {
        Animator doorAnimation = GetComponent<Animator>(); ;
        doorAnimation.SetTrigger("Open Door");
    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this door
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "Open Door";
}
