using UnityEngine;

/// <summary>
/// Class to play sounds with animation events
/// </summary>
public class SoundOnAnimationEvent : MonoBehaviour
{
    /// <summary>
    /// Plays a sound
    /// </summary>
    /// <param name="sound">Sound to be played</param>
    public void PlaySound(SoundClip sound)
    {
        SoundManager.PlaySound(sound);
    }
}
