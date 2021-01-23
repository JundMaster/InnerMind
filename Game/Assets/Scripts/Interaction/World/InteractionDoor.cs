using UnityEngine;

/// <summary>
/// Class for every interactable door that the player can use
/// Extends InteractionCommon
/// </summary>
public class InteractionDoor : InteractionCommon
{
    private Animator doorAnimation;

    private void Start()
    {
       doorAnimation = GetComponent<Animator>(); 
    }
    /// <summary>
    /// This method determines the action of the door when clicked
    /// </summary>
    public override void Execute()
    {
        if(!doorAnimation.GetBool("isOpen"))
        {
            doorAnimation.SetBool("isOpen", true);
            doorAnimation.SetTrigger("Open Door");
        }
       else
        {
            doorAnimation.SetBool("isOpen", false);
            doorAnimation.SetTrigger("Close Door");
        }

    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this door
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => doorAnimation.GetBool("isOpen") ?
        "Close Door" : "Open Door";
}
