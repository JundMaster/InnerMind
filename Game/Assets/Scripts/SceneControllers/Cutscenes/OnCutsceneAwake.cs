using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Class for every cutscene.
/// </summary>
public class OnCutsceneAwake : MonoBehaviour
{
    // Components
    private IPlayerInput input;
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
    /// If player presses pass scene key, an event is triggered.
    /// The skin is skipped and loads loading screen for next scene.
    /// </summary>
    private void Update()
    {
        if (sceneChange)
        {
            if (input.Space)
            {
                File.AppendAllText(FilePath.watchedCutscenes,
                                            $"\n{sceneChange.GoToScene}");

                OnChangedScene(sceneChange.GoToScene);
            }
        } 
    }

    /// <summary>
    /// Method that invokes ChangeScene event.
    /// </summary>
    /// <param name="scene">Scene to change to</param>
    protected virtual void OnChangedScene(SceneNames scene)
        => ChangedScene?.Invoke(scene);

    /// <summary>
    /// Event that happens when a scene is changed.
    /// </summary>
    public event Action<SceneNames> ChangedScene;
}
