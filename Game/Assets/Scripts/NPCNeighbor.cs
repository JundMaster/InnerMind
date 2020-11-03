using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class NPCNeighbor : NPCInteractable
{
    public override IEnumerator InteractionAction()
    {
        CR_RunningCoroutine = false;
        yield return null;
    }
}
