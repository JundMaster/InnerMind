using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3_1 : PuzzleBase
{
    [SerializeField] private GameObject doorWithCode;
    private InteractionDoorWithCode doorScriptPuzzle;

    private void Awake()
    {
        player = FindObjectOfType<PlayerGeneralInfo>();
        doorScriptPuzzle = doorWithCode.GetComponentInChildren<InteractionDoorWithCode>();
    }

    private void Start()
    {
        if (player.PuzzlesDone.HasFlag(myPuzzle))
            doorWithCode.GetComponentInChildren<Animator>().SetTrigger("Open Door");
    }

    private void OnEnable()
    {
        doorScriptPuzzle.DoorOpened += base.Victory;
    }
    private void OnDisable()
    {
        doorScriptPuzzle.DoorOpened -= base.Victory;
    }
}
