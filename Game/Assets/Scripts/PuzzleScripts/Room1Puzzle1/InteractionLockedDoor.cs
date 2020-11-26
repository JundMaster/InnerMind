using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLockedDoor : InteractionCommon
{
    private InteractionMedicineCabinet cabinet;
    private void Start()
    {
        cabinet = FindObjectOfType<InteractionMedicineCabinet>();
    }
    public override void Execute()
    {
        if (cabinet.IsOpen)
        {
            Animator doorAnimation = GetComponent<Animator>(); ;
            doorAnimation.SetTrigger("Open Door");
        }
        else Debug.Log("You need to get your meds");
    }
    public override string ToString() => "Open Door";
}
