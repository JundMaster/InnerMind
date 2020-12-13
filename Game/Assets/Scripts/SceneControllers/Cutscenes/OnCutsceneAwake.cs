using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Class for every cutscene
/// </summary>
sealed public class OnCutsceneAwake : MonoBehaviour
{
    // Components
    private PlayerInput input;
    [SerializeField] private SceneChange sceneChange;

    /// <summary>
    /// Awake method for OnCutsceneAwake
    /// </summary>
    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();

        input.ChangeTypeOfControl(TypeOfControl.InCutscene);
    }

    /// <summary>
    /// Update method for OnCutsceneAwake.
    /// If player presses pass scene key, the scene skips
    /// </summary>
    private void Update()
    {
        if (sceneChange)
        {
            if (input.PassScene)
            {
                File.AppendAllText(FilePath.watchedCutscenes,
                                            $"\n{sceneChange.GoToScene}");

                SceneManager.LoadScene(sceneChange.GoToScene.ToString());
            }
        } 
    }
}
