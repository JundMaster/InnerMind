using UnityEngine;

/// <summary>
/// Class for sliders in options pause menu
/// </summary>
public class OptionSlidersController : MonoBehaviour
{
    /// <summary>
    /// Adjusts mouse speed
    /// </summary>
    /// <param name="speed">Value of the slider</param>
    public void AdjustMouseSpeed(float speed)
    {
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        PlayerPrefs.SetFloat("mouseSpeed", playerInput.MouseSpeed = speed);
    }

    /// <summary>
    /// Adjusts sound volume
    /// </summary>
    /// <param name="speed">Value of the slider</param>
    public void AdjustSoundVolume(float sound)
    {

    }

    /// <summary>
    /// Adjusts music volume
    /// </summary>
    /// <param name="speed">Value of the slider</param>
    public void AdjustMusicVolume(float music)
    {

    }
}
