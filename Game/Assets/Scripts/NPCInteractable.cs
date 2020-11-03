using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCInteractable : NPC, IInterectableController
{
    // IInterectableController
    public Coroutine ThisCoroutine { get; protected set; }

    public bool CR_RunningCoroutine { get; protected set; }

    public abstract IEnumerator InteractionAction();


    public void RunCoroutine()
    {
        if (CR_RunningCoroutine == false)
        {
            ThisCoroutine = StartCoroutine(InteractionAction());
            CR_RunningCoroutine = true;
        }
    }
}
