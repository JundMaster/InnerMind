using System.Collections;
using UnityEngine;

public abstract class InteractionNPCBase : InteractionCR
{
    // Wait for seconds instance
    protected YieldInstruction waitForSecs;
    [SerializeField] protected float secondsToWait;

    // This npc speak counter
    protected byte speakCounter;

    // Dialog for a npc
    protected DialogText dialog;

    public abstract override IEnumerator CoroutineInteraction();
}
