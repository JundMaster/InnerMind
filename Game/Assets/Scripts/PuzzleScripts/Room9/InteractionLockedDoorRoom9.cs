using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for the locked Door in the 1st Room
/// </summary>
public class InteractionLockedDoorRoom9 : InteractionCommon
{
    //Components
    private Room9 room9;

    /// <summary>
    /// Start method of InteractionLockedDoor
    /// </summary>
    private void Awake()
    {
        room9 = FindObjectOfType<Room9>();

    }

    /// <summary>
    /// This method determines the action of the door when clicked
    /// </summary>
    public override void Execute()
    {
        room9.VictoryCheck();
    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "Open Door";

}
