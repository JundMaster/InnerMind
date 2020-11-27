using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3_2 : PuzzleBase
{
    [SerializeField] private GameObject doorNeighbor;
    [SerializeField] private ScriptableItem[] puzzlePianoKeys;

    private void Start()
    {
        // QUANDO HOUVER AS KEYS REVER O PUZZLE // 
        //////////////////////////////////////////

        // Checks if player finished puzzle4
        if (inventory.Bag.Contains(puzzlePianoKeys[0]) &&
            inventory.Bag.Contains(puzzlePianoKeys[1]) &&
            inventory.Bag.Contains(puzzlePianoKeys[2]))
            base.Victory();

        if (player.PuzzlesDone.HasFlag(myPuzzle))
        {
            doorNeighbor.GetComponentInChildren<Animator>().SetTrigger("Open Door");
        }
    }
}
