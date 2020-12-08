using UnityEngine;

/// <summary>
/// Class responsible for puzzle 2 in room 3
/// </summary>
public class Room3_1 : PuzzleBase
{
    // Door with code in editor
    [SerializeField] private GameObject doorWithCode;

    // Door with code script
    private InteractionDoorWithCode doorScriptPuzzle;

    /// <summary>
    /// Awake method of Room3_1
    /// </summary>
    private void Awake()
    {
        // This awake has to exist because it's different from base awake

        player = FindObjectOfType<PlayerGeneralInfo>();

        doorScriptPuzzle = 
            doorWithCode.GetComponentInChildren<InteractionDoorWithCode>();

        // Reads the file after 0.25 second (from base)
        if (readPuzzlesDoneTxtCoroutine == null)
            readPuzzlesDoneTxtCoroutine = StartCoroutine(ReadPuzzlesDoneTxt());
    }

    /// <summary>
    /// OnEnable method of Room3_1
    /// </summary>
    private void OnEnable()
    {
        // Subscribes to an event
        doorScriptPuzzle.DoorOpened += this.Victory;
    }

    /// <summary>
    /// OnDisable method of Room3_1
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribes to an event
        doorScriptPuzzle.DoorOpened -= this.Victory;
    }

    /// <summary>
    /// Does an action when the puzzle is solved
    /// Opens a door
    /// </summary>
    public override void Victory()
    {
        base.Victory();
        doorWithCode.GetComponentInChildren<Animator>().SetTrigger("Open Door");
    }
}
