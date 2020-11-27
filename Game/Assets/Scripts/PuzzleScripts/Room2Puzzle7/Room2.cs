using UnityEngine;
using System.Collections;

public class Room2 : PuzzleBase
{
    [SerializeField] private GameObject prize;
    [SerializeField] private ScriptableItem prizeScriptableItem;
    [SerializeField] private Transform prizePosition;
    private MirrorPuzzleCubeParent[] cubeParentsInRoom;

    public bool FinishedPuzzle { get; private set; }

    private void Start()
    {
        cubeParentsInRoom = GetComponentsInChildren<MirrorPuzzleCubeParent>();
        FinishedPuzzle = false;

        // If player has puzzle done and no map on inventory, plays victory
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
        sbyte counter;
        counter = 0;
        foreach (MirrorPuzzleCubeParent cubeParent in cubeParentsInRoom)
            if (cubeParent.InCorrectPosition)
                counter++;

        if (counter == cubeParentsInRoom.Length)
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
