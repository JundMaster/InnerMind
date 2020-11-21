using UnityEngine;
using UnityEngine.SceneManagement;

sealed public class CutsceneController : MonoBehaviour, ICutsceneController
{
    [SerializeField] private string sceneToLoad;


    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }
}
