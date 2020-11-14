using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Interaction_DoorWithCode : Interaction_Common
{
    [SerializeField] private int code;

    // Component
    private PlayerInput input;

    // Code Input



    public override void Execute()
    {
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InDoorWithCode);
    }

    public void GetInput(string userInput)
    {
        /*
        if (input.Enter)
        {
            if (Int32.TryParse(userInput, out int userConvertedInput))
            {
                if (userConvertedInput == code)
                {
                    Animator doorAnimation = GetComponentInParent<Animator>();
                    doorAnimation.SetTrigger("Open Door");
                }
            }
        }*/
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InGameplay);
    }

    private void Start()
    {
        input = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {

    }


    public override string ToString() => "Open Door";
}
