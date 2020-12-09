using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for controlling room1 puzzle. Extends PuzzleBase
/// </summary>
public class Room1 : PuzzleBase
{
    //Components
    InteractionMedicineCabinet cabinet;

    /// <summary>
    /// Property that checks if the puzzle was finished
    /// </summary>
    public bool FinishedPuzzle { get; private set; }

    /// <summary>
    /// Start method of Room1
    /// </summary>
    private void Start()
    {
        cabinet = FindObjectOfType<InteractionMedicineCabinet>();
        FinishedPuzzle = false;
    }

    /// <summary>
    /// Update method of Room1
    /// </summary>
    private void Update()
    {
        Victory();
    }


    /// <summary>
    /// Does an action when the puzzle is solved
    /// </summary>
    public override void Victory()
    {
        base.Victory();
        if (cabinet.IsOpen)
        {
            FinishedPuzzle = true;
        }
    }
}
