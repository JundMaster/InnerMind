using UnityEngine;

/// <summary>
/// Class for SoundManager
/// </summary>
sealed public class SoundManager : MonoBehaviour
{
    // Sound Clips
    private static AudioClip[] sounds;

    // Variable to control audiosource
    private static AudioSource audioSource;
        
    // Variable to control volume
    public float SoundVolume { get; set; }

    /// <summary>
    /// Start method for sound manager
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        sounds = new AudioClip[1];

        sounds[0] = Resources.Load<AudioClip>("Sounds/footstep");
    }

    /// <summary>
    /// Update method for sound manager
    /// </summary>
    private void Update()
    {
        audioSource.volume = SoundVolume;
    }

    /// <summary>
    /// Plays a sound depending on the clip parameter
    /// </summary>
    /// <param name="clip">Sound to play</param>
    public static void PlaySound(SoundClip clip)
    {
        switch (clip)
        {
            case SoundClip.footstep:
                audioSource.PlayOneShot(sounds[0], 1f);
                break;
        }
    }
}
