using UnityEngine;
using System.IO;

public class Room3 : MonoBehaviour
{
    [SerializeField] private Transform[] spawns;

    private string roomEntered;
    private PlayerMovement player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();

        // Loads file written in SceneChange script
        if (File.Exists(FilePath.lastScenePath))
        {
            using (StreamReader sr = File.OpenText(FilePath.lastScenePath))
            {
                roomEntered = sr.ReadLine();
            }
        }
    }

    private void Start()
    {
        switch(roomEntered)
        {
            case "InRoom2":
                player.transform.position = spawns[1].transform.position;
                player.transform.rotation = spawns[1].transform.rotation;
                break;
            case "InRoom5":
                player.transform.position = spawns[2].transform.position;
                player.transform.rotation = spawns[2].transform.rotation;
                break;
            case "InAmbulanceCutscene":
                player.transform.position = spawns[2].transform.position;
                player.transform.rotation = spawns[2].transform.rotation;
                break;
            // On first respawn
            default:
                player.transform.position = spawns[0].transform.position;
                player.transform.rotation = spawns[0].transform.rotation;
                break;
        }   
    }
}
