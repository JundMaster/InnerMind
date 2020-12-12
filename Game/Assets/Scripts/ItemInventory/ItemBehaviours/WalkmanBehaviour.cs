using UnityEngine;

/// <summary>
/// Class for Walkman Behaviour. Implements IUsable
/// </summary>
public class WalkmanBehaviour : MonoBehaviour, IUsable
{
    /// <summary>
    /// Determins WalkmanBehaviour action when used.
    /// If the player uses the walkman, it plays a sound.
    /// </summary>
    public void Use()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>();

        // Plays walkmanAudio only if the audio isn't already playing
        if (soundManager.AudioSource.isPlaying == false)
            SoundManager.PlaySound(SoundClip.walkmanAudio);
    }
}
