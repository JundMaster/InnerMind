using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Components
    private SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InPauseMenu);
        sceneController = new SceneController();
    }
    
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        sceneController.LoadGameScene(SceneList.InteractionTest);
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InGameplay);
    }

    public void QuitGame()
    {
        Debug.Log("You left the game!");
        
        //Used to exit the actual build;
        //Application.Quit();
    }

    
}
