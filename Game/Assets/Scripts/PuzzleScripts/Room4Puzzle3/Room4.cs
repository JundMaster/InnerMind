﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for controlling room4 puzzle. Extends PuzzleBase
/// </summary>
public class Room4 : PuzzleBase, ICoroutineT<string>
{
    //Components
    private GetPlayOrder keyPlayOrder;
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private ItemComparer itemComparer;

    //Inspector Variable
    [SerializeField] private GameObject prize;
    [SerializeField] private Transform prizePosition;
    [SerializeField] private string thought;
    [SerializeField] private GameObject thoughtCanvas;
    [SerializeField] private BoxCollider roomDoor;
    [SerializeField] private Animator[] pianoAnim;

    /// <summary>
    /// Property used to see if the player has finished the puzzle
    /// </summary>
    public bool FinishedPuzzle { get; private set; }

    /// <summary>
    /// Property to control a coroutine
    /// </summary>
    public Coroutine ThisCoroutine { get; private set; }

    /// <summary>
    /// Start method of Room4
    /// </summary>
    private void Start()
    {
        itemComparer = FindObjectOfType<ItemComparer>();
        keyPlayOrder = FindObjectOfType<GetPlayOrder>();
        FinishedPuzzle = false;
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        waitForSeconds = new WaitForSeconds(3);
        ThisCoroutine = null;

        // If player has puzzle done and no audiotape in inventory, plays victory
        if (player.PuzzlesDone.HasFlag(myPuzzle) &&
            inventory.Bag.Contains(itemComparer.NotRewoundAudioTape) == false)
        {
            Victory();
        }
    }

    /// <summary>
    /// Does an action when the puzzle is solved
    /// </summary>
    public override void Victory()
    {
        for(byte i = 0; i < pianoAnim.Length; i++)
        {
            
            pianoAnim[i].gameObject.GetComponentInParent<BoxCollider>().enabled = false;
            pianoAnim[i].SetTrigger("Victory");
        }
        
        roomDoor.enabled = false;
        base.Victory();
        StartCoroutine(VictoryCoroutine());
    }

    /// <summary>
    /// Checks if the victory conditions are true
    /// </summary>
    public void VictoryCheck()
    {
        //Puzzle's Solution Input
        int[] solution = new int[] { 3, 3, 1 };

        //Checks if the players input is the same as the solution
        if (solution[0] == keyPlayOrder.PianoPlayerInput.x &&
            solution[1] == keyPlayOrder.PianoPlayerInput.y &&
            solution[2] == keyPlayOrder.PianoPlayerInput.z)
        {
            Victory();
        }
        else
        {
            if (ThisCoroutine == null)
            {
                ThisCoroutine = StartCoroutine(CoroutineExecute(thought));
            }

        }
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

        if (FindObjectOfType<Inventory>().Bag.Contains(
            itemComparer.NotRewoundAudioTape) == false)
        {
            spawn = Instantiate(
                prize, prizePosition.transform.position, Quaternion.identity);
        }

        while (spawn)
        {
            spawn.transform.Rotate(15 * Time.deltaTime, 0, 15 * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }

    /// <summary>
    /// Renders a thought during a few seconds
    /// </summary>
    /// <returns>Returns paused time in seconds</returns>
    public IEnumerator CoroutineExecute(string thought)
    {
        thoughtCanvas.SetActive(true);
        displayText.text = thought;
        displayText.enabled = true;
        yield return waitForSeconds;
        displayText.enabled = false;
        thoughtCanvas.SetActive(false);
        ThisCoroutine = null;
    }
}