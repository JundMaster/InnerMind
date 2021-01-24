using UnityEngine;
using System.IO;

/// <summary>
/// Class responsible for Room3.
/// </summary>
public class Room3 : PuzzleBase
{
    // Possible player spawns in editor
    // Because the player can respawn from 5 of the 6 doors in the room
    [SerializeField] private Transform[] spawns;
    [SerializeField] private Animator anim;

    // This string is used to keep the current room entered from room 3
    private string roomEntered;

    // Components
    private PlayerMovement movement;

    /// <summary>
    /// Start method of Room3.
    /// </summary>
    private void Start()
    {
        movement = FindObjectOfType<PlayerMovement>();

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

        // The player will spawn on a position depending on the last 
        // room entered
        switch (roomEntered)
        {
            case "InRoom2":
                movement.transform.position = spawns[1].transform.position;
                movement.transform.rotation = spawns[1].transform.rotation;
                break;

            case "InRoom5":
                movement.transform.position = spawns[2].transform.position;
                movement.transform.rotation = spawns[2].transform.rotation;
                break;

            case "InAmbulanceCutscene":
                movement.transform.position = spawns[2].transform.position;
                movement.transform.rotation = spawns[2].transform.rotation;
                break;

            case "InRoom4":
                movement.transform.position = spawns[3].transform.position;
                movement.transform.rotation = spawns[3].transform.rotation;
                break;

            case "InRoom6":
                movement.transform.position = spawns[4].transform.position;
                movement.transform.rotation = spawns[4].transform.rotation;
                break;

            case "InJohanneCutscene":
                movement.transform.position = spawns[4].transform.position;
                movement.transform.rotation = spawns[4].transform.rotation;
                break;

            // On first respawn
            default:
                movement.transform.position = spawns[0].transform.position;
                movement.transform.rotation = spawns[0].transform.rotation;
                break;
        }
    }

    /// <summary>
    /// Method that calls victory from base class.
    /// </summary>
    public override void Victory()
    {
        base.Victory();
    }
}
