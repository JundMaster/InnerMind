using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Door : MonoBehaviour, IInteractable
{
    public void InteractionAction()
    {
        Animator doorAnimation = GetComponent<Animator>();;
        doorAnimation.SetTrigger("openDoor");
    }

    public override string ToString() => "Open Door";
}
