using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : InteractionCommon
{
    public override void Execute()
    {
        Animator doorAnimation = GetComponent<Animator>(); ;
        doorAnimation.SetTrigger("Open Door");
    }

    public override string ToString() => "Open Door";
}
