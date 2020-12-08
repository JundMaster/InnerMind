using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4 : PuzzleBase
{
    [SerializeField] private GameObject prize;
    [SerializeField] private ScriptableItem prizeScriptableItem;
    [SerializeField] private Transform prizePosition;

    private GetPlayOrder keyPlayOrder;

    public bool FinishedPuzzle { get; private set; }

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

    public override void Victory()
    {
        base.Victory();
        StartCoroutine(VictoryCoroutine());
    }

    public void VictoryCheck()
    {
        int[] solution = new int[] { 3, 2, 1 };

        if (solution[0] == keyPlayOrder.PianoPlayerInput.x &&
            solution[1] == keyPlayOrder.PianoPlayerInput.y &&
            solution[2] == keyPlayOrder.PianoPlayerInput.z)
        {
            Victory();
        }
    }

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
