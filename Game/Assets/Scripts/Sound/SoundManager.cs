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

    /// <summary>
    /// Property for audiosource
    /// </summary>
    public AudioSource AudioSource { get => audioSource; 
                                    private set => audioSource = value; }

    /// <summary>
    /// Propert for SoundVolume. Controls volume
    /// </summary>
    public float SoundVolume { get; set; }

    /// <summary>
    /// Start method for sound manager
    /// </summary>
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();

        sounds = new AudioClip[16];

        sounds[0] = Resources.Load<AudioClip>("Sounds/footstep");
        sounds[1] = Resources.Load<AudioClip>("Sounds/walkmanAudio");
        sounds[2] = Resources.Load<AudioClip>("Sounds/flashlightClick");
        sounds[3] = Resources.Load<AudioClip>("Sounds/wallSlide");
        sounds[4] = Resources.Load<AudioClip>("Sounds/MajorAKeyNote");
        sounds[5] = Resources.Load<AudioClip>("Sounds/MajorCKeyNote");
        sounds[6] = Resources.Load<AudioClip>("Sounds/MajorEKeyNote");
        sounds[7] = Resources.Load<AudioClip>("Sounds/doorOpen");
        sounds[8] = Resources.Load<AudioClip>("Sounds/drawerOpen");
        sounds[9] = Resources.Load<AudioClip>("Sounds/drawerClosing");
        sounds[10] = Resources.Load<AudioClip>("Sounds/pickUpObject");
        sounds[11] = Resources.Load<AudioClip>("Sounds/padlockWheel");
        sounds[12] = Resources.Load<AudioClip>("Sounds/padlockOpen");
        sounds[13] = Resources.Load<AudioClip>("Sounds/cubeRotating");
        sounds[14] = Resources.Load<AudioClip>("Sounds/audioTapeRewind");
        sounds[15] = Resources.Load<AudioClip>("Sounds/batteryInsert");
    }

    /// <summary>
    /// Update method for sound manager
    /// </summary>
    private void Update()
    {
        AudioSource.volume = SoundVolume;
    }

    /// <summary>
    /// Plays a sound depending on the clip parameter
    /// </summary>
    /// <param name="clip">Sound to play</param>
    public static void PlaySound(SoundClip clip)
    {
        switch (clip)
        {
            case SoundClip.Footstep:
                audioSource.PlayOneShot(sounds[0], 1f);
                break;
            case SoundClip.WalkmanAudio:
                audioSource.PlayOneShot(sounds[1], 1f);
                break;
            case SoundClip.FlashlightClick:
                audioSource.PlayOneShot(sounds[2], 1f);
                break;
            case SoundClip.WallSlide:
                audioSource.PlayOneShot(sounds[3], 0.5f);
                break;
            case SoundClip.MajorAKeyNote:
                audioSource.PlayOneShot(sounds[4], 0.4f);
                break;
            case SoundClip.MajorCKeyNote:
                audioSource.PlayOneShot(sounds[5], 0.4f);
                break;
            case SoundClip.MajorEKeyNote:
                audioSource.PlayOneShot(sounds[6], 0.4f);
                break;
            case SoundClip.DoorOpen:
                audioSource.PlayOneShot(sounds[7], 0.6f);
                break;
            case SoundClip.DrawerOpen:
                audioSource.PlayOneShot(sounds[8], 0.3f);
                break;
            case SoundClip.DrawerClosing:
                audioSource.PlayOneShot(sounds[9], 0.3f);
                break;
            case SoundClip.PickUpObject:
                audioSource.PlayOneShot(sounds[10], 0.25f);
                break;
            case SoundClip.PadlockWheel:
                audioSource.PlayOneShot(sounds[11], 0.8f);
                break;
            case SoundClip.PadlockOpened:
                audioSource.PlayOneShot(sounds[12], 0.8f);
                break;
            case SoundClip.CubeRotating:
                audioSource.PlayOneShot(sounds[13], 0.9f);
                break;
            case SoundClip.AudioTapeRewind:
                audioSource.PlayOneShot(sounds[14], 1f);
                break;
            case SoundClip.BatteryInsert:
                audioSource.PlayOneShot(sounds[15], 0.8f);
                break;
            case SoundClip.WallSlideLower:
                audioSource.PlayOneShot(sounds[3], 0.3f);
                break;
        }
    }
}
