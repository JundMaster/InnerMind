using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private SceneNames goToScene;

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name.Equals("Room3"))
        {
            using (StreamWriter sw = File.CreateText(FilePath.lastScenePath))
            {
                sw.WriteLine($"In{goToScene}");
            }
        }

        if (other.CompareTag("Player"))
            SceneManager.LoadScene(goToScene.ToString());
    }

    private void OnApplicationQuit()
    {
        File.Delete(FilePath.lastScenePath);
    }
}
