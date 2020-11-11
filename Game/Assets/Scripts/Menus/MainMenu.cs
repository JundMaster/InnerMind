using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

   
    // Components
    private PlayerInput input;
    private GraphicRaycaster graphicRaycaster;
    private SceneController sceneController;
    [SerializeField] private GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = new SceneController();
        input = FindObjectOfType<PlayerInput>();
        graphicRaycaster = GetComponent<GraphicRaycaster>();
    }

    public void StartGame()
    {
        sceneController.LoadGameScene(SceneList.InteractionTest);
    }

    public void QuitGame()
    {
        Debug.Log("You left the game!");
        
        //Used to exit the actual build;
        //Application.Quit();
    }

    
}
