using UnityEngine;
using System.Collections;

/// <summary>
/// Class responsible for Room5 puzzle. Extends PuzzleBase
/// </summary>
public class Room5 : PuzzleBase
{
    // Variable with every movingWall parent on editor
    [SerializeField] private GameObject movingWalls;

    // Variable with animator from the last wall on the puzzle on editor
    [SerializeField] private Animator lastWallAnimator;

    // Map related variables/////////////////////////////////////////////
    [SerializeField] private Sprite mapImage;
    [SerializeField] private GameObject mapCanvas;

    // Items to compare
    [SerializeField] private ScriptableItem flaslight;
    [SerializeField] private ScriptableItem noBatteryFlashlight;
    [SerializeField] private ScriptableItem pianoKey2;

    // Objects to destroy
    [SerializeField] private GameObject flashlightSpawn;
    [SerializeField] private GameObject pianoKeySpawn;

    /// <summary>
    /// Start method for Room5
    /// </summary>
    private void Start()
    {
        StartCoroutine(CheckItemSpawn());
    }

    /// <summary>
    /// Update method of Room5
    /// </summary>
    private void Update()
    {
        // If the last wall animation hide wall is playing
        if (lastWallAnimator.GetCurrentAnimatorStateInfo(0).IsName("hideWall"))
        {
            // If the animation reached its end
            if (lastWallAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                // Players victory
                this.Victory();
            }
        }
    }

    /// <summary>
    /// Does an action when the puzzle is solved
    /// </summary>
    public override void Victory()
    {
        base.Victory();

        // After running base.Victory(), hides every walls
        movingWalls.SetActive(false);
    }

    private IEnumerator CheckItemSpawn()
    {
        yield return new WaitForSeconds(0.25f);

        if (inventory.Bag.Contains(flaslight) ||
            inventory.Bag.Contains(noBatteryFlashlight))
            Destroy(flashlightSpawn);

        if (inventory.Bag.Contains(pianoKey2) ||
            player.PuzzlesDone.HasFlag(PuzzlesEnum.Puzzle3))
            Destroy(pianoKeySpawn);
    }
}