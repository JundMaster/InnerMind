using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3 : MonoBehaviour
{
    [SerializeField] private GameObject doorWithCodePuzzle2;
    private InteractionDoorWithCode doorScriptPuzzle2;

    [SerializeField] private GameObject doorPuzzle4;
    [SerializeField] private ScriptableItem[] puzzle4PianoKeys;

    private PuzzlesEnum myPuzzle2, myPuzzle4;

    private PlayerGeneralInfo player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerGeneralInfo>();
        doorScriptPuzzle2 = doorWithCodePuzzle2.GetComponentInChildren<InteractionDoorWithCode>();
    }

    private void Start()
    {
        myPuzzle2 = PuzzlesEnum.Puzzle2;
        myPuzzle4 = PuzzlesEnum.Puzzle4;

        // Checks if player finished puzzle4
        if (FindObjectOfType<Inventory>().Bag.Contains(puzzle4PianoKeys[0]) &&
            FindObjectOfType<Inventory>().Bag.Contains(puzzle4PianoKeys[1]) &&
            FindObjectOfType<Inventory>().Bag.Contains(puzzle4PianoKeys[2]))
            Puzzle4Victory();

        if (player.PuzzlesDone.HasFlag(myPuzzle2))
            doorWithCodePuzzle2.GetComponentInChildren<Animator>().SetTrigger("Open Door");

        if (player.PuzzlesDone.HasFlag(myPuzzle4))
        {
            doorPuzzle4.GetComponentInChildren<Animator>().SetTrigger("Open Door");
        }
    }


    private void OnEnable()
    {
        doorScriptPuzzle2.DoorOpened += Puzzle2Victory;
    }
    private void OnDisable()
    {
        doorScriptPuzzle2.DoorOpened -= Puzzle2Victory;
    }

    private void Puzzle4Victory()
    {
        player.PuzzlesDone |= myPuzzle4;
    }

    private void Puzzle2Victory()
    {
        player.PuzzlesDone |= myPuzzle2;
    }
}
