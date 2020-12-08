using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract Class for every interactable NPC that the player can interact with
/// Extends InteractionCR
/// </summary>
public abstract class InteractionNPCBase : InteractionCR
{
    // Wait for seconds instance
    protected YieldInstruction waitForSecs;
    [SerializeField] protected float secondsToWait;

    // This npc speak counter
    // It's the number of times the player has spoken to the npc
    protected byte speakCounter;

    // Dialog for the npc
    protected DialogText dialog;

    /// <summary>
    /// This Coroutine determines the action of the NPC when clicked
    /// </summary>
    /// <returns>Returns null</returns>
    public abstract override IEnumerator CoroutineExecute();
}
