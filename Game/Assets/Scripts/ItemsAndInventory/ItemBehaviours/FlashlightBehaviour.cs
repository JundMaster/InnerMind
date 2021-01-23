using UnityEngine;

/// <summary>
/// Class for Flashlight Behaviour. Implements IUsable.
/// </summary>
public class FlashlightBehaviour : MonoBehaviour, IUsable
{
    // Flashlight
    private Light flashlight;

    /// <summary>
    /// OnDisable method for Flashlight behaviour.
    /// </summary>
    private void OnDisable()
    {
        flashlight.range = 7;
        flashlight.spotAngle = 120;
        flashlight.intensity = 0.3f;
    }

    /// <summary>
    /// Determines FlashlightBehaviour action when used.
    /// If the player uses the flashlight, the light gets stronger.
    /// </summary>
    public void Use()
    {      
        flashlight = GameObject.FindGameObjectWithTag("PlayerLantern").
            GetComponent<Light>();

        SoundManager.PlaySound(SoundClip.FlashlightClick);

        // Turns on flashlight
        if (flashlight.range == 7)
        {
            flashlight.range = 10;
            flashlight.spotAngle = 90;
            flashlight.intensity = 1.5f;
        }
        else // Turns off flaslight
        {
            flashlight.range = 7;
            flashlight.spotAngle = 120;
            flashlight.intensity = 0.3f;
        }
    }
}
