using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Start method for MainMenu
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// Loads first scene
    /// Called on new game button
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("IntroCutscene");
    }

    /// <summary>
    /// Quits the game
    /// Called on quit button
    /// </summary>
    public void QuitGame()
    {       
        Application.Quit();
    }

    
}
