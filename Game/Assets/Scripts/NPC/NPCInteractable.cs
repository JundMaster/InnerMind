using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCInteractable : NPC, IInterectableController
{
    // IInterectableController
    public Coroutine ThisCoroutine { get; set; }

    public abstract IEnumerator InteractionAction();
    //

    // How many texts in npc
    [SerializeField] protected int numberOfTexts;

    // Wait for seconds instance
    protected YieldInstruction waitForSecs;
    [SerializeField] protected float secondsToWait;


    // Runs the npc coroutine
    public void RunCoroutine()
    {
        if (ThisCoroutine == null)
        {
            ThisCoroutine = StartCoroutine(InteractionAction());
        }
    }
}
