using UnityEngine;
using System.Collections;

public class Room2 : MonoBehaviour
{
    [SerializeField] private GameObject prize;
    [SerializeField] private ScriptableItem prizeScriptableItem;
    [SerializeField] private Transform prizePosition;
    private MirrorPuzzleCubeParent[] cubeParentsInRoom;

    public bool FinishedPuzzle { get; private set; }

    private PuzzlesEnum myPuzzle;
    private void Start()
    {
        myPuzzle = PuzzlesEnum.Puzzle2;
        cubeParentsInRoom = GetComponentsInChildren<MirrorPuzzleCubeParent>();
        FinishedPuzzle = false;

        // If player has puzzle done and no map on inventory, plays victory
        if (FindObjectOfType<PlayerGeneralInfo>().PuzzlesDone.HasFlag(myPuzzle) &&
            FindObjectOfType<Inventory>().Bag.Contains(prizeScriptableItem) == false)
        {
            StartCoroutine(Victory());
        }
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
            StartCoroutine(Victory());
        }
    }

    private IEnumerator Victory()
    {
        FindObjectOfType<PlayerGeneralInfo>().PuzzlesDone |= PuzzlesEnum.Puzzle2;
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
