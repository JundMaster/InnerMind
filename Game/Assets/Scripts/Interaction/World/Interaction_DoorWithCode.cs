using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Interaction_DoorWithCode : Interaction_Common
{
    // Codes to open padlock
    [SerializeField] private Vector3Int doorCode;
    public Vector3Int DoorCode => doorCode;
    public Vector3Int UserCode { get; set; }

    // Padlock to spawn
    [SerializeField] private GameObject padlock;
    private GameObject newPadlock;

    // Component
    private PlayerInput input;

    // Padlock control related variables
    [SerializeField] private GameObject padlockCanvas;
    [SerializeField] private Transform padlockPosition;

    private void Start()
    {
        input = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (PlayerInput.CurrentControl == TypeOfControl.InDoorWithCode)
        {
            if (input.RightClick)
            {
                Destroy(newPadlock);
                padlockCanvas.SetActive(false);
                PlayerInput.ChangeTypeOfControl(TypeOfControl.InGameplay);
            }
        }
    }

    public override void Execute()
    {
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InDoorWithCode);
        padlockCanvas.SetActive(true);

        UserCode = new Vector3Int(0, 0, 0);
        newPadlock = Instantiate(padlock);
        newPadlock.transform.position = padlockPosition.position;
        newPadlock.transform.rotation = padlockPosition.rotation;
    }

    public void OpenDoor()
    {
        Animator doorAnimation = GetComponentInParent<Animator>();
        doorAnimation.SetTrigger("Open Door");

        Destroy(newPadlock);
        padlockCanvas.SetActive(false);
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InGameplay);
    }

    public void BackToGameplay()
    {
        Destroy(newPadlock);
        padlockCanvas.SetActive(false);
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InGameplay);
    }

    public override string ToString() => "Open Locked Door";
}
