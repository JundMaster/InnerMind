using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCInteractable : NPC, IInterectableController
{
    // IInterectableController
    public Coroutine ThisCoroutine { get; set; }

    public bool CR_RunningCoroutine { get; protected set; }

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
        if (CR_RunningCoroutine == false)
        {
            ThisCoroutine = StartCoroutine(InteractionAction());
            CR_RunningCoroutine = true;
        }
    }
}
