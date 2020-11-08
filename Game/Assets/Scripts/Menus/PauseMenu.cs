using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool Gamepaused { get; private set; }

    // Components
    private PlayerInput input;
    private Animator anim;

    // Variable to control last menu before pause
    TypeOfControl lastTypeOfControl;

    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
        anim = GetComponentInChildren<Animator>();

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

            Gamepaused = !Gamepaused;
        }

        anim.SetBool("paused", Gamepaused);
    }
}
