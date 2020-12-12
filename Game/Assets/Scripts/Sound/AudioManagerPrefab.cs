using UnityEngine;

/// <summary>
/// Class for AudioManagerPrefab. Applies Don'tDestroyOnLoad
/// </summary>
public class AudioManagerPrefab : MonoBehaviour
{
    public static AudioManagerPrefab instance = null;

    /// <summary>
    /// Awake method for AudioManagerPrefab
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
