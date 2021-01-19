using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/// <summary>
/// Class for pause menu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    // Gamepaused property
    public bool Gamepaused { get; private set; }

    // Menus inside PauseMenu
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject assistMenu;
    [SerializeField] private GameObject mainMenuConfirmation;

    // Components
    private PlayerInput input;
    private GraphicRaycaster graphicRaycaster;

    // Variable to control last menu before pause
    private TypeOfControl lastTypeOfControl;

    // PauseGameEvent event
    public event Action PauseGameEvent;

    /// <summary>
    /// Awake method for PauseMenu
    /// </summary>
    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        Gamepaused = false;
    }


    /// <summary>
    /// Update method for PauseMenu
    /// </summary>
    private void Update()
    {
        PauseGame();
    }

    /// <summary>
    /// Method to invoke PauseGameEvent
    /// </summary>
    private void OnPauseGame()
        => PauseGameEvent?.Invoke();

    /// <summary>
    /// Method to pause and unpause the game
    /// </summary>
    private void PauseGame()
    {
        // If the player presses pause key
        if (input.Pause)
        {
            // If game is NOT paused, it pauses the game
            if (Gamepaused == false)
            {
                OnPauseGame();

                Time.timeScale = 0f;

                pauseMenu.SetActive(true);

                // Sets cursor icon to default
                Cursor.SetCursor(default, input.CursorPosition,
                                            CursorMode.Auto);

                // Keeps control before paused in a variable
                // so when the player unpauses, the class knows what type
                // of control it should change to
                lastTypeOfControl = input.CurrentControl;

                input.ChangeTypeOfControl(TypeOfControl.InPauseMenu);
            }
            // If game IS paused, it unpauses the game
            else if (Gamepaused)
            {
                Time.timeScale = 1f;

                pauseMenu.SetActive(false);
                optionsMenu.SetActive(false);
                assistMenu.SetActive(false);
                mainMenuConfirmation.SetActive(false);

                // Changes type of control back to the last state before the 
                // game was paused
                input.ChangeTypeOfControl(lastTypeOfControl);
            }

            // Turns on/off graphic raycast and Gamepaused variable
            graphicRaycaster.enabled = !graphicRaycaster.enabled;
            Gamepaused = !Gamepaused;
        }
    }

    /// <summary>
    /// Method used to resume game, only called when the user presses
    /// Resume Game button
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        // Changes type of control back to the last state
        input.ChangeTypeOfControl(lastTypeOfControl);

        // Turns on/off graphic raycast and Gamepaused variable
        graphicRaycaster.enabled = !graphicRaycaster.enabled;
        Gamepaused = !Gamepaused;
    }

    /// <summary>
    /// Method used to return to MainMenu, only called when the user presses
    /// Main Menu button
    /// </summary>
    public void MainMenu()
    {
        Gamepaused = false;
        input.ChangeTypeOfControl(TypeOfControl.InPauseMenu);  
        SceneManager.LoadScene("MainMenu");
    }
}
