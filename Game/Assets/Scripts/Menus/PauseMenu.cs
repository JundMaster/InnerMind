using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    // Gamepaused property
    public static bool Gamepaused { get; private set; }

    // Menus game objects
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;


    // Components
    private PlayerInput input;
    private GraphicRaycaster graphicRaycaster;

    private SceneController sceneController;

    // Variable to control last menu before pause
    TypeOfControl lastTypeOfControl;


    private void Awake()
    {
        sceneController = new SceneController();
        input = FindObjectOfType<PlayerInput>();
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

                pauseMenu.SetActive(true);

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

                pauseMenu.SetActive(false);
                optionsMenu.SetActive(false);


                // Changes type of control back to the last state
                PlayerInput.ChangeTypeOfControl(lastTypeOfControl);
            }

            // Turns on/off graphic raycast and Gamepaused variable
            graphicRaycaster.enabled = !graphicRaycaster.enabled;
            Gamepaused = !Gamepaused;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        // Changes type of control back to the last state
        PlayerInput.ChangeTypeOfControl(lastTypeOfControl);

        // Turns on/off graphic raycast and Gamepaused variable
        graphicRaycaster.enabled = !graphicRaycaster.enabled;
        Gamepaused = !Gamepaused;
    }
    public void MainMenu()
    {
        sceneController.LoadGameScene(SceneList.MainMenuTest);
    }
}
