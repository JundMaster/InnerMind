using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1 : PuzzleBase
{
    InteractionMedicineCabinet cabinet;

    public bool FinishedPuzzle { get; private set; }

   
    private void Start()
    {
        cabinet = FindObjectOfType<InteractionMedicineCabinet>();
        FinishedPuzzle = false;

    }

    private void Update()
    {
        Victory();
    }

    public override void Victory()
    {
        base.Victory();
        if (cabinet.IsOpen)
        {
            FinishedPuzzle = true;
        }
    }
}
