using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Door : MonoBehaviour, IInteractable
{
    private Animator doorAnimation;
    public void InteractionAction()
    {
        doorAnimation = GetComponent<Animator>();
        doorAnimation.SetTrigger("openDoor");
    }

    public override string ToString() => "Open Door";
}
