using UnityEngine;
using System.IO;

/// <summary>
/// Class responsible for Room3
/// </summary>
public class Room3 : MonoBehaviour
{
    // Possible player spawns in editor
    // Because the player can respawn from 5 of the 6 doors in the room
    [SerializeField] private Transform[] spawns;

    // This string is used to keep the current room entered from room 3
    private string roomEntered;

    // Components
    private PlayerMovement player;

    /// <summary>
    /// Awake method for Room3
    /// </summary>
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();

        // Loads file written in SceneChange script.
        // RoomEntered will be the same as that line in the file.
        // This file will be written everytime the player leaves room 3 and
        //  enters a new room
        if (File.Exists(FilePath.lastScenePath))
        {
            using (StreamReader sr = File.OpenText(FilePath.lastScenePath))
            {
                roomEntered = sr.ReadLine();
            }
        }
    }

    /// <summary>
    /// Start method of Room3
    /// </summary>
    private void Start()
    {
        // The player will spawn on a position depending on the last 
        // room entered
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
