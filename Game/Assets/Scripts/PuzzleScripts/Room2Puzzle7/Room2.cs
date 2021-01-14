﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Class responsible for controlling room2 puzzle. Extends PuzzleBase
/// </summary>
public class Room2 : PuzzleBase
{
    // Prize related variables in inspector
    [SerializeField] private GameObject prize;
    [SerializeField] private Transform prizePosition;
    [SerializeField] private Light prizeLight;
    [SerializeField] private GameObject roomCandle;

    // Variable with every cube parents in room
    private MirrorPuzzleCubeParent[] cubeParentsInRoom;

    /// <summary>
    /// Property that checks if the puzzle was finished
    /// </summary>
    public bool FinishedPuzzle { get; private set; }

    private ItemComparer itemComparer;

    /// <summary>
    /// Start method of Room2
    /// </summary>
    private void Start()
    {
        cubeParentsInRoom = GetComponentsInChildren<MirrorPuzzleCubeParent>();
        FinishedPuzzle = false;
        itemComparer = FindObjectOfType<ItemComparer>();

        // If player has puzzle done and no prize on inventory, plays victory
        // This happens for example if the player leaves the room without
        // picking the prize, and then returns to the room
        if (player.PuzzlesDone.HasFlag(myPuzzle) &&
            inventory.Bag.Contains(itemComparer.PianoKey1) == false)
        {
            Victory();
        }
    }

    /// <summary>
    /// Checks if the victory conditions are true
    /// </summary>
    public void VictoryCheck()
    {
        sbyte counter;
        counter = 0;

        // Checks if every cube is in correct position
        foreach (MirrorPuzzleCubeParent cubeParent in cubeParentsInRoom)
            if (cubeParent.InCorrectPosition)
                counter++;

        // If every cube is in correct position, plays Victory()
        if (counter == cubeParentsInRoom.Length)
        {
            Victory();
        }
    }

    /// <summary>
    /// Does an action when the puzzle is solved
    /// </summary>
    public override void Victory()
    {
        base.Victory();

        // After doing base.Victory, does aditional actions
        StartCoroutine(VictoryCoroutine());
    }

    /// <summary>
    /// Does an action when the puzzle is solved.
    /// Sets finished puzzle to true and spawns a rotating prize until
    /// the player picks it up
    /// </summary>
    /// <returns>Returns null</returns>
    private IEnumerator VictoryCoroutine()
    {
        FinishedPuzzle = true;
        GameObject spawn = null;
        roomCandle.SetActive(false);
        prizeLight.intensity = 1f;

        if (FindObjectOfType<Inventory>().Bag.Contains(
            itemComparer.PianoKey1) == false &&
            player.PuzzlesDone.HasFlag(PuzzlesEnum.Puzzle3) == false)
        {
            spawn = Instantiate(
                prize, prizePosition.transform.position, Quaternion.identity);
        }
        
        // While the item is active, it keeps rotating
        while (spawn)
        {
            spawn.transform.Rotate(15 * Time.deltaTime, 0, 15 * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }
}
