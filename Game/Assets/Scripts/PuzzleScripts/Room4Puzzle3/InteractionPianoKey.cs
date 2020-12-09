using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class used when interacting with piano keys
/// </summary>
public class InteractionPianoKey : InteractionCommon
{
    //Component
    private Animator animator;

    //Inspector Variables
    [SerializeField] private PianoKeyID keyID;
    

    /// <summary>
    /// Start method InteractionPianoKey
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    /// <summary>
    /// This method determines the action of the piano key when clicked
    /// </summary>
    public override void Execute()
    {
        animator.SetTrigger("PlayKey");
        KeyID?.Invoke(keyID);
    }

    //KeyID event with played key's ID
    public event Action<PianoKeyID> KeyID;

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "PlayKey";

}
