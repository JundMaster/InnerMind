using UnityEngine;
using UnityEngine.UI;

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
    // ScriptableItem with a map, to compare if the player has one
    [SerializeField] private ScriptableItem mapScriptableObject;
    /// /////////////////////////////////////////////////////////////////

    // ScriptableItem with a flashlight, to compare if the player has one
    [SerializeField] private ScriptableItem flashlightScriptableObject;

    // Components
    private Light flashlight;

    /// <summary>
    /// Start method for Room5
    /// </summary>
    private void Start()
    {
        // If player has the puzzle's map
        if (inventory.Bag.Contains(mapScriptableObject))
        {
            // Sets the map canvas active. The player will see a map on screen
            mapCanvas?.SetActive(true);
            mapCanvas.GetComponentInChildren<Image>().sprite = mapImage;
        }else { }

        // If player has a flashlight with battery on inventory
        if (inventory.Bag.Contains(flashlightScriptableObject))
        {
            // Turns the player's light stronger
            flashlight = GameObject.FindGameObjectWithTag("PlayerLantern").
                GetComponent<Light>();
            flashlight.range = 8;
            flashlight.spotAngle = 80;
            flashlight.intensity = 1;
        } else { }
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
}
