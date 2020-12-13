using UnityEngine;
using System.IO;

/// <summary>
/// Class common to every scene ingame. Destroys txt files.
/// </summary>
public class FileDestroyer : MonoBehaviour
{
    /// <summary>
    /// OnApplicationQuit for SceneChange
    /// </summary>
    private void OnApplicationQuit()
    {
        File.Delete(FilePath.lastScenePath);
        File.Delete(FilePath.watchedCutscenes);
        File.Delete(FilePath.inventoryPath);
        File.Delete(FilePath.puzzlePath);
    }
}
