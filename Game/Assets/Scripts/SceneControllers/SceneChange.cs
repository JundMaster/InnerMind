using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Class responsible for changing scenes
/// </summary>
public class SceneChange : MonoBehaviour
{
    // First scene to change the scene to
    [SerializeField] private SceneNames goToScene;

    // If the goToScene already happened, this will be the scene to change to
    [SerializeField] private SceneNames elseGoToScene;

    /// <summary>
    /// OnTriggerEnter of SceneChange
    /// When leaving room 3, writes player's position to a file
    /// When the player collides with this collider, it loads a new scene
    /// </summary>
    /// <param name="other">Collider the SceneChange collided with</param>
    private void OnTriggerEnter(Collider other)
    {
        // Saves room3 last position before entering a new room
        if (SceneManager.GetActiveScene().name.Equals("Room3") ||
            SceneManager.GetActiveScene().name.Equals("Room7"))
        {
            using (StreamWriter sw = File.CreateText(FilePath.lastScenePath))
            {
                sw.WriteLine($"In{goToScene}");
            }
        }
        
        // On collision with player
        if (other.CompareTag("Player"))
        {
            // If file exists
            if (File.Exists(FilePath.watchedCutscenes))
            {
                bool watchedCutscene = false;
                string watchedCutsceneStr = null;

                // Reads everyline and compares if the line is the same as goTo
                using (StreamReader sr = File.OpenText(FilePath.watchedCutscenes))
                {
                    while ((watchedCutsceneStr = sr.ReadLine()) != null)
                    {
                        // If goToScene already happened, sets watchedCutscene
                        //  to true
                        if (watchedCutsceneStr == goToScene.ToString())
                            watchedCutscene = true;
                    }                   
                }
                // After reading the file

                // If goToScene was watched, loads elseGoToScreen
                if (watchedCutscene)
                    SceneManager.LoadScene(elseGoToScene.ToString());

                // Else, if the goToScene wasn't watched, loads the normal 
                //  cutscene order and writes its name on the file
                else
                {
                    File.AppendAllText(FilePath.watchedCutscenes, $"\n{goToScene}");

                    SceneManager.LoadScene(goToScene.ToString());
                }
            }

            // If the file doesn't exist, creates a new file
            else
            {
                File.AppendAllText(FilePath.watchedCutscenes, $"{goToScene}");

                SceneManager.LoadScene(goToScene.ToString());
            }
        }
    }

    /// <summary>
    /// OnApplicationQuit for SceneChange
    /// </summary>
    private void OnApplicationQuit()
    {
        File.Delete(FilePath.lastScenePath);
        File.Delete(FilePath.watchedCutscenes);
    }
}
