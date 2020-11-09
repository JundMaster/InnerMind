using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool Gamepaused { get; private set; }

    // Components
    private PlayerInput input;
    private Animator anim;
    private GraphicRaycaster graphicRaycaster;

    // Variable to control last menu before pause
    TypeOfControl lastTypeOfControl;

    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
        anim = GetComponentInChildren<Animator>();
        graphicRaycaster = GetComponent<GraphicRaycaster>();

        Gamepaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        if (input.Pause)
        {
            // If game is paused
            if (Gamepaused == false)
            {
                Time.timeScale = 0f;

                // Sets cursor icon to default
                Cursor.SetCursor(default, PlayerInput.CursorPosition,
                                            CursorMode.Auto);

                // Keeps control before paused in a variable
                lastTypeOfControl = PlayerInput.CurrentControl;

                PlayerInput.ChangeTypeOfControl(TypeOfControl.InPauseMenu);
            }
            else if (Gamepaused)
            {
                Time.timeScale = 1f;

                // Changes type of control back to the last state
                PlayerInput.ChangeTypeOfControl(lastTypeOfControl);
            }

            // Turns on/off graphic raycast and Gamepaused variable
            graphicRaycaster.enabled = !graphicRaycaster.enabled;
            Gamepaused = !Gamepaused;
        }

        anim.SetBool("paused", Gamepaused);
    }
}
