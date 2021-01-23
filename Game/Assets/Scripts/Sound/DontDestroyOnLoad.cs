using UnityEngine;

/// <summary>
/// Class for DontDestroyOnLoad.
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad instance = null;

    /// <summary>
    /// Awake method for DontDestroyOnLoad
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
