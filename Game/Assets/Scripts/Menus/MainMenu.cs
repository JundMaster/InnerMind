using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("InteractionTest");
    }

    public void QuitGame()
    {
        Debug.Log("You left the game!");
        
        //Used to exit the actual build;
        //Application.Quit();
    }

    
}
