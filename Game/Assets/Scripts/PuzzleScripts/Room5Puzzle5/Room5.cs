
using UnityEngine;
using UnityEngine.UI;

public class Room5 : PuzzleBase
{
    [SerializeField] private GameObject movingWalls;
    [SerializeField] private Animator lastWallAnimator;

    [SerializeField] private Sprite mapImage;
    [SerializeField] private GameObject mapCanvas;
    [SerializeField] private ScriptableItem mapScriptableObject;

    private void Start()
    {
        // If puzzle is done
        if (player.PuzzlesDone.HasFlag(myPuzzle))
        {
            movingWalls.SetActive(false);
        }
        // If player has the puzzle's map
        if (inventory.Bag.Contains(mapScriptableObject))
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
                this.Victory();
            }
        }
        if (player.PuzzlesDone.HasFlag(myPuzzle))
        {
            movingWalls.SetActive(false);
        }
    }

    public override void Victory()
    {
        base.Victory();
        movingWalls.SetActive(false);
    }
}
