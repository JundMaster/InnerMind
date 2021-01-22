using UnityEngine;
using System.Collections;

/// <summary>
/// Class responsible for Room5 puzzle. Extends PuzzleBase.
/// </summary>
public class Room5 : PuzzleBase
{
    // Variable with every movingWall parent on editor
    [SerializeField] private GameObject movingWalls;

    // Variable with animator from the last wall on the puzzle on editor
    [SerializeField] private Animator lastWallAnimator;

    // Objects to destroy
    [SerializeField] private GameObject flashlightSpawn;
    [SerializeField] private GameObject pianoKeySpawn;
    [SerializeField] private GameObject oldBatterySpawn;

    // Components
    private ItemComparer comparer;

    /// <summary>
    /// Start method for Room5.
    /// </summary>
    private void Start()
    {
        comparer = FindObjectOfType<ItemComparer>();
        StartCoroutine(CheckItemSpawn());
    }

    /// <summary>
    /// Update method of Room5.
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
    /// Does an action when the puzzle is solved.
    /// </summary>
    public override void Victory()
    {
        base.Victory();

        // After running base.Victory(), hides every walls
        movingWalls.SetActive(false);
    }

    /// <summary>
    /// If the player has a certain item, that item is not spawned 
    /// (destroyed) in this scene.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckItemSpawn()
    {
        yield return new WaitForSeconds(0.25f);

        if (inventory.Bag.Contains(comparer.FlashLight) ||
            inventory.Bag.Contains(comparer.NoBatteryFlashlight))
            Destroy(flashlightSpawn);

        if (inventory.Bag.Contains(comparer.OldBattery) ||
            inventory.Bag.Contains(comparer.NoBatteryFlashlight))
            Destroy(oldBatterySpawn);

        if (inventory.Bag.Contains(comparer.PianoKey2) ||
            player.PuzzlesDone.HasFlag(PuzzlesEnum.Puzzle3))
            Destroy(pianoKeySpawn);
    }
}