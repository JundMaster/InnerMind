using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Class responsible for loading loadingscene and setting the next scene to
/// load.
/// </summary>
public class LoadingHandler : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen; 

    private Coroutine loadSceneCoroutine;

    // Components to register events
    private SceneChange[] sceneChangers;
    private OnCutsceneAwake cutscene;
    private MainMenu mainMenu;

    /// <summary>
    /// OnEnable method for LoadingHandler, registers to events.
    /// </summary>
    private void OnEnable()
    {
        sceneChangers = FindObjectsOfType<SceneChange>();
        cutscene = FindObjectOfType<OnCutsceneAwake>();
        mainMenu = FindObjectOfType<MainMenu>();

        if (sceneChangers != null)
        {
            foreach (SceneChange sc in sceneChangers)
                sc.ChangedScene += SetLoadingScene;
        }

        if (cutscene != null) cutscene.ChangedScene += SetLoadingScene;

        if (mainMenu != null) mainMenu.ChangedScene += SetLoadingScene;
    }

    /// <summary>
    /// OnDisable method for LoadingHandler, unregisters from events.
    /// </summary>
    private void OnDisable()
    {
        if (sceneChangers != null)
        {
            foreach (SceneChange sc in sceneChangers)
                sc.ChangedScene -= SetLoadingScene;
        }

        if (cutscene != null) cutscene.ChangedScene -= SetLoadingScene;

        if (mainMenu != null) mainMenu.ChangedScene -= SetLoadingScene;
    }

    /// <summary>
    /// Method that starts a coroutine to load the next scene.
    /// </summary>
    /// <param name="scene">Next scene to load</param>
    private void SetLoadingScene(SceneNames scene)
    {
        loadingScreen.gameObject.SetActive(true);

        if (loadSceneCoroutine == null)
            loadSceneCoroutine = StartCoroutine(
                LoadNewScene(scene));
    }

    /// <summary>
    /// Coroutine that loads a new scene.
    /// </summary>
    /// <param name="scene">Scene to load.</param>
    /// <returns>Returns null.</returns>
    private IEnumerator LoadNewScene(SceneNames scene)
    {
        YieldInstruction waitForFrame = new WaitForEndOfFrame();
        AsyncOperation sceneToLoad = 
            SceneManager.LoadSceneAsync(scene.ToString());

        // After the progress reaches 1, the scene loads
        while (sceneToLoad.progress < 1)
        {
            yield return waitForFrame;
        }

        loadSceneCoroutine = null;
    }
}
