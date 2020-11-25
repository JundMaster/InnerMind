
using UnityEngine;
using UnityEngine.UI;

public class Room5 : MonoBehaviour
{
    private PuzzlesEnum myPuzzle;
    [SerializeField] private GameObject movingWalls;
    [SerializeField] private Animator lastWallAnimator;

    [SerializeField] private Sprite mapImage;
    [SerializeField] private GameObject mapCanvas;
    [SerializeField] private ScriptableItem mapScriptableObject;

    private void Start()
    {
        FindObjectOfType<Inventory>().Bag.Add(mapScriptableObject);
        myPuzzle = PuzzlesEnum.Puzzle5;
        // If puzzle is done
        if (FindObjectOfType<PlayerGeneralInfo>().PuzzlesDone.HasFlag(myPuzzle))
        {
            movingWalls.SetActive(false);
        }
        // If player has the puzzle's map
        if (FindObjectOfType<Inventory>().Bag.Contains(mapScriptableObject))
        {
            mapCanvas?.SetActive(true);
            mapCanvas.GetComponentInChildren<Image>().sprite = mapImage;
        }else { }
    }

    private void Update()
    {
        if (lastWallAnimator.GetCurrentAnimatorStateInfo(0).IsName("hideWall"))
        {
            if (lastWallAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Victory();
            }
        }
    }

    private void Victory()
    {
        FindObjectOfType<PlayerGeneralInfo>().PuzzlesDone |= myPuzzle;
        movingWalls.SetActive(false);
    }
}
