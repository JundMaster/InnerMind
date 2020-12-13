using UnityEngine;

/// <summary>
/// Class for Flashlight Behaviour. Implements IUsable
/// </summary>
public class FlashlightBehaviour : MonoBehaviour, IUsable
{
    // Flashlight
    private Light flashlight;

    // Bool to control if flashlight is active
    private bool flashlightActive;

    /// <summary>
    /// Start method for Flashlight Behaviour
    /// </summary>
    private void Start()
    {
        flashlightActive = false;
    }

    /// <summary>
    /// Determins FlashlightBehaviour action when used.
    /// If the player uses the flashlight, the light gets stronger.
    /// </summary>
    public void Use()
    {
        flashlight = GameObject.FindGameObjectWithTag("PlayerLantern").
            GetComponent<Light>();

        SoundManager.PlaySound(SoundClip.FlashlightClick);

        if (flashlightActive == false)
        {
            flashlight.range = 10;
            flashlight.spotAngle = 90;
            flashlight.intensity = 1.5f;
            flashlightActive = true;
        }
        else
        {
            flashlight.range = 7;
            flashlight.spotAngle = 120;
            flashlight.intensity = 0.6f;
            flashlightActive = false;
        }
    }
}
