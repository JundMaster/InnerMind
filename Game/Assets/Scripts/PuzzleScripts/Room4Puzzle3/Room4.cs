using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for controlling room4 puzzle. Extends PuzzleBase
/// </summary>
public class Room4 : PuzzleBase
{
    //Components
    private GetPlayOrder keyPlayOrder;

    //Inspector Variable
    [SerializeField] private GameObject prize;
    [SerializeField] private ScriptableItem prizeScriptableItem;
    [SerializeField] private Transform prizePosition;

    /// <summary>
    /// Property used to see if the player has finished the puzzle
    /// </summary>
    public bool FinishedPuzzle { get; private set; }

    /// <summary>
    /// Start method of Room4
    /// </summary>
    private void Start()
    {
        keyPlayOrder = FindObjectOfType<GetPlayOrder>();
        FinishedPuzzle = false;


        // If player has puzzle done and no audiotape in inventory, plays victory
        if (player.PuzzlesDone.HasFlag(myPuzzle) &&
            inventory.Bag.Contains(prizeScriptableItem) == false)
        {
            Victory();
        }
    }

    /// <summary>
    /// Does an action when the puzzle is solved
    /// </summary>
    public override void Victory()
    {
        base.Victory();
        StartCoroutine(VictoryCoroutine());
    }

    /// <summary>
    /// Checks if the victory conditions are true
    /// </summary>
    public void VictoryCheck()
    {
        //Puzzle's Solution Input
        int[] solution = new int[] { 3, 2, 1 };

        //Checks if the players input is the same as the solution
        if (solution[0] == keyPlayOrder.PianoPlayerInput.x &&
            solution[1] == keyPlayOrder.PianoPlayerInput.y &&
            solution[2] == keyPlayOrder.PianoPlayerInput.z)
        {
            Victory();
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
            prizeScriptableItem) == false)
        {
            spawn = Instantiate(prize, prizePosition.transform.position, Quaternion.identity);
        }

        while (spawn)
        {
            spawn.transform.Rotate(15 * Time.deltaTime, 0, 15 * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }
}
