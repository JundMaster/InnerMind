using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// Class used when interacting with piano keys
/// </summary>
public class InteractionPianoKey : InteractionCommon
{
    //Inspector Variables
    [SerializeField] private Animator keyAnimator;
    [SerializeField] private PianoKeyID keyID;

    //Variable used to see if the player can play a key
    private bool canPlay;

    private void Start()
    {
        canPlay = true;
    }

    private void Update()
    {
        //Checks if the animator is still running
        if (keyAnimator.GetCurrentAnimatorStateInfo(0).IsName("PianoKey"))
        {
            
            // If the animation reached its end
            if (keyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98)
            {

                canPlay = true;
            }
        }
    }

    /// <summary>
    /// This method determines the action of the piano key when clicked
    /// </summary>
    public override void Execute()
    {
        if (canPlay)
        {
            keyAnimator.SetTrigger("playKey");
            KeyID?.Invoke(keyID);

            switch (keyID)
            {
                case PianoKeyID.Key1:
                    SoundManager.PlaySound(SoundClip.MajorAKeyNote);
                    break;
                case PianoKeyID.Key2:
                    SoundManager.PlaySound(SoundClip.MajorCKeyNote);
                    break;
                case PianoKeyID.Key3:
                    SoundManager.PlaySound(SoundClip.MajorEKeyNote);
                    break;
            }

            canPlay = false;
        }

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
