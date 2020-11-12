using System;
using System.Collections;
using UnityEngine;

public class NPC_Interaction_test : NPC_InteractionBase
{
    protected override IEnumerator CoroutineInteraction()
    {
        float elapsedTime = 0;
        while (elapsedTime < 1.5f)
        {
            transform.position += new Vector3(0.5f, 0, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
}