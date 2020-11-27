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

        // Reads the file after 1 second
        if (readPuzzlesDoneTxtCoroutine == null)
            readPuzzlesDoneTxtCoroutine = StartCoroutine(ReadPuzzlesDoneTxt());
    }

    private void OnEnable()
    {
        doorScriptPuzzle.DoorOpened += this.Victory;
    }
    private void OnDisable()
    {
        doorScriptPuzzle.DoorOpened -= this.Victory;
    }

    public override void Victory()
    {
        base.Victory();
        doorWithCode.GetComponentInChildren<Animator>().SetTrigger("Open Door");
    }
}
