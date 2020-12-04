
using UnityEngine;
using UnityEngine.UI;

public class Room5 : PuzzleBase
{
    [SerializeField] private GameObject movingWalls;
    [SerializeField] private Animator lastWallAnimator;

    [SerializeField] private Sprite mapImage;
    [SerializeField] private GameObject mapCanvas;
    [SerializeField] private ScriptableItem mapScriptableObject;

    [SerializeField] private ScriptableItem lanternScriptableObject;
    private Light lantern;

    private void Start()
    {
        // If player has the puzzle's map
        if (inventory.Bag.Contains(mapScriptableObject))
        {
            mapCanvas?.SetActive(true);
            mapCanvas.GetComponentInChildren<Image>().sprite = mapImage;
        }else { }

        // If player has a lantern with battery on inventory
        if (inventory.Bag.Contains(lanternScriptableObject))
        {
            lantern = GameObject.FindGameObjectWithTag("PlayerLantern").
                GetComponent<Light>();
            lantern.range = 8;
            lantern.spotAngle = 80;
            lantern.intensity = 1;
        }
        else { }
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
    }

    public override void Victory()
    {
        base.Victory();
        movingWalls.SetActive(false);
    }
}
