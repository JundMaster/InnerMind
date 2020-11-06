using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCInteractable : NPC, IInterectableController
{
    // IInterectableController
    public Coroutine ThisCoroutine { get; set; }

    public abstract IEnumerator InteractionAction();
    //


    // Runs the npc coroutine
    public void RunCoroutine()
    {
        PlayerInput input = FindObjectOfType<PlayerInput>();

        input.CurrentControl = TypeOfControl.InNPCInteraction;

        if (ThisCoroutine == null)
        {
            ThisCoroutine = StartCoroutine(InteractionAction());
        }
    }
}
