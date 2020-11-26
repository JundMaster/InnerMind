using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1 : MonoBehaviour
{
    [SerializeField] private GameObject prize;
    [SerializeField] private ScriptableItem prizeScriptableItem;
    [SerializeField] private Transform prizePosition;

    public bool FinishedPuzzle { get; private set; }

    private PuzzlesEnum myPuzzle;
    private void Start()
    {
        myPuzzle = PuzzlesEnum.Puzzle1;
        FinishedPuzzle = false;

        // If player has puzzle done and no map on inventory, plays victory
        if (FindObjectOfType<PlayerGeneralInfo>().PuzzlesDone.HasFlag(myPuzzle) &&
            FindObjectOfType<Inventory>().Bag.Contains(prizeScriptableItem) == false)
        {
            StartCoroutine(Victory());
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Victory()
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
