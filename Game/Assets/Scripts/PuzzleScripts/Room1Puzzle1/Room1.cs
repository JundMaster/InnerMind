using UnityEngine;
using System;
/// <summary>
/// Class responsible for controlling room1 puzzle. Extends PuzzleBase
/// </summary>
public class Room1 : PuzzleBase
{

    /// <summary>
    /// Property that checks if the puzzle was finished
    /// </summary>
    public bool FinishedPuzzle { get; private set; }

    /// <summary>
    /// Awake method of Room1. This awake is overriding puzzlebase Awake
    /// </summary>
    private void Awake()
    {
        player = FindObjectOfType<PlayerGeneralInfo>();
        inventory = FindObjectOfType<Inventory>();
        FinishedPuzzle = false;
    }

    /// <summary>
    /// OnEnable method for Room1
    /// </summary>
    private void OnEnable()
    {
        inventory.Bag.AddedItem += OnPillsGrab;
    }

    private void OnPillsGrab(ScriptableItem item)
    {
        if (inventory.Bag.Contains(item)) Victory();
    }

    /// <summary>
    /// Does an action when the puzzle is solved
    /// </summary>
    public override void Victory()
    {
        base.Victory();
        FinishedPuzzle = true;
    }

    /// <summary>
    /// OnDisable method for Room1
    /// </summary>
    private void OnDisable()
    {
        inventory.Bag.AddedItem -= OnPillsGrab;
    }
}
