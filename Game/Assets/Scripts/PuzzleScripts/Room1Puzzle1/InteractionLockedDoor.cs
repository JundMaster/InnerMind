using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLockedDoor : InteractionCommon
{

    private Room1 room1;
    private void Start()
    {
        room1 = FindObjectOfType<Room1>();
    }
    public override void Execute()
    {

        if (room1.FinishedPuzzle)
        {
            Animator doorAnimation = GetComponent<Animator>(); ;
            doorAnimation.SetTrigger("Open Door");
        }
        
    }
    public override string ToString() => "Open Door";
}
