using UnityEngine;

/// <summary>
/// Class for MusicManager
/// </summary>
public class MusicManager : MonoBehaviour
{
    // Variable to control audiosource
    private AudioSource audioSource;

    /// <summary>
    /// Propert for SoundVolume. Controls volume
    /// </summary>
    public float MusicVolume { get; set; }

    /// <summary>
    /// Start method for MusicManager
    /// </summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Update method for MusicManager
    /// </summary>
    private void Update()
    {
        // Sets volume = music volume
        audioSource.volume = MusicVolume;
    }
}
