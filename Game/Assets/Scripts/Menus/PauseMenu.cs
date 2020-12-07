using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PauseMenu : MonoBehaviour
{
    // Gamepaused property
    public bool Gamepaused { get; private set; }

    // Menus game objects
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;


    // Components
    private PlayerInput input;
    private GraphicRaycaster graphicRaycaster;


    // Variable to control last menu before pause
    private TypeOfControl lastTypeOfControl;


    public event Action PauseGameEvent;

    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
        graphicRaycaster = GetComponent<GraphicRaycaster>();

        Gamepaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        PauseGame();
    }

    private void OnPauseGame()
        => PauseGameEvent?.Invoke();

    private void PauseGame()
    {
        if (input.Pause)
        {
            // If game is paused
            if (Gamepaused == false)
            {
                OnPauseGame();

                Time.timeScale = 0f;

                pauseMenu.SetActive(true);

                // Sets cursor icon to default
                Cursor.SetCursor(default, input.CursorPosition,
                                            CursorMode.Auto);

                // Keeps control before paused in a variable
                lastTypeOfControl = input.CurrentControl;

                input.ChangeTypeOfControl(TypeOfControl.InPauseMenu);
            }
            else if (Gamepaused)
            {
                Time.timeScale = 1f;

                pauseMenu.SetActive(false);
                optionsMenu.SetActive(false);


                // Changes type of control back to the last state
                input.ChangeTypeOfControl(lastTypeOfControl);
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
        input.ChangeTypeOfControl(lastTypeOfControl);

        // Turns on/off graphic raycast and Gamepaused variable
        graphicRaycaster.enabled = !graphicRaycaster.enabled;
        Gamepaused = !Gamepaused;
    }
    public void MainMenu()
    {
        Gamepaused = false;
        input.ChangeTypeOfControl(TypeOfControl.InPauseMenu);
        SceneManager.LoadScene("MainMenuTest");
    }
}
