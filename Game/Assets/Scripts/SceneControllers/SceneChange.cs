using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private SceneNames goToScene;
    [SerializeField] private SceneNames elseGoToScene;

    private void OnTriggerEnter(Collider other)
    {
        // Saves room3 last position before entering a new room
        if (SceneManager.GetActiveScene().name.Equals("Room3"))
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
                        if (watchedCutsceneStr == goToScene.ToString())
                            watchedCutscene = true;
                    }                   
                }
    
                // If cutscene was watched, loads elseGoToScreen
                if (watchedCutscene)
                    SceneManager.LoadScene(elseGoToScene.ToString());

                // Else loads the normal cutscene order
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

    private void OnApplicationQuit()
    {
        File.Delete(FilePath.lastScenePath);
        File.Delete(FilePath.watchedCutscenes);
    }
}
