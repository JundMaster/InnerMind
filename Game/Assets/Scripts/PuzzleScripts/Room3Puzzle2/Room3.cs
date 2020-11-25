using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3 : MonoBehaviour
{
    [SerializeField] private GameObject doorWithCode;
    private InteractionDoorWithCode doorScript;

    private PuzzlesEnum myPuzzle2, myPuzzle4;

    private PlayerGeneralInfo player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerGeneralInfo>();
        doorScript = doorWithCode.GetComponentInChildren<InteractionDoorWithCode>();
    }

    private void Start()
    {
        myPuzzle2 = PuzzlesEnum.Puzzle2;
        myPuzzle4 = PuzzlesEnum.Puzzle4;

        if (player.PuzzlesDone.HasFlag(myPuzzle2))
            doorWithCode.GetComponentInChildren<Animator>().SetTrigger("Open Door");

        if (player.PuzzlesDone.HasFlag(myPuzzle4))
        {    // CODE HERE //
        }
    }


    private void OnEnable()
    {
        doorScript.DoorOpened += Puzzle2Victory;
    }
    private void OnDisable()
    {
        doorScript.DoorOpened -= Puzzle2Victory;
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
