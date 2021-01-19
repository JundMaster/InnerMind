using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Class to control post processing
/// </summary>
public class PostProcessing : MonoBehaviour
{
    // Post process volume var
    [SerializeField] private PostProcessVolume postProcess;

    // Effects to control
    private ColorGrading colorGrading;

    /// <summary>
    /// Property for brightness value
    /// </summary>
    public float BrightnessValue { get; set; }

    /// <summary>
    /// Property for contrast value
    /// </summary>
    public float ContrastValue { get; set; }

    private void Awake()
    {
        postProcess.profile.TryGetSettings(out colorGrading);
    }

    /// <summary>
    /// Updated method for postprocessing. Updates values
    /// </summary>
    private void Update()
    {
        colorGrading.postExposure.value = BrightnessValue;
        colorGrading.contrast.value = ContrastValue;
    }
}
