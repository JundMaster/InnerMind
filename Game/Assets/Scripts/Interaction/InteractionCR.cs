using System.Collections;
using UnityEngine;
public abstract class InteractionCR : InteractionCommon, ICoroutine
{
    public Coroutine ThisCoroutine { get; set;}
    public override void Execute()
    {
        if (ThisCoroutine == null)
            ThisCoroutine = StartCoroutine(CoroutineInteraction());
    }

    public abstract IEnumerator CoroutineInteraction();
}