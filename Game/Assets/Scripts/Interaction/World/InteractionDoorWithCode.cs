using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InteractionDoorWithCode : InteractionCommon
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
        if (input.CurrentControl == TypeOfControl.InDoorWithCode)
        {
            if (input.RightClick)
            {
                Destroy(newPadlock);
                padlockCanvas.SetActive(false);
                input.ChangeTypeOfControl(TypeOfControl.InGameplay);
            }
        }
    }

    public override void Execute()
    {
        if (UserCode != doorCode)
        {
            input.ChangeTypeOfControl(TypeOfControl.InDoorWithCode);
            padlockCanvas.SetActive(true);

            UserCode = new Vector3Int(0, 0, 0);
            newPadlock = Instantiate(padlock);
            newPadlock.transform.position = padlockPosition.position;
            newPadlock.transform.rotation = padlockPosition.rotation;
        }
    }

    public void OpenDoor()
    {
        Animator doorAnimation = GetComponentInParent<Animator>();
        doorAnimation.SetTrigger("Open Door");

        OnDoorOpened();

        Destroy(newPadlock);
        padlockCanvas.SetActive(false);
        input.ChangeTypeOfControl(TypeOfControl.InGameplay);
    }

    public void BackToGameplay()
    {
        Destroy(newPadlock);
        padlockCanvas.SetActive(false);
        input.ChangeTypeOfControl(TypeOfControl.InGameplay);
    }

    private void OnDoorOpened()
    {
        DoorOpened?.Invoke();
    }

    public event Action DoorOpened;

    public override string ToString() => "Open Locked Door";
}
