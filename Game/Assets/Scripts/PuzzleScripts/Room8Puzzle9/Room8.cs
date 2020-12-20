using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for controlling the puzzle of the room 8
/// </summary>
public class Room8 : PuzzleBase
{
    [SerializeField] private GameObject prize;
    [SerializeField] private WallLampsParent wallLampsParent;
    [SerializeField] private Animator doorAnimator;

    // Components
    private ItemComparer itemComparer;

    /// <summary>
    /// Start method for Room8
    /// </summary>
    private void Start()
    {
        itemComparer = FindObjectOfType<ItemComparer>();

        wallLampsParent.LampsAligned += Victory;
        prize.SetActive(false);
    }

    /// <summary>
    /// Executes the actions for when the puzzle is solved
    /// </summary>
    public override void Victory()
    {
        foreach (WallLamp lamp in wallLampsParent.Lamps)
        {
            Destroy(lamp.GetComponent<WallLampInteraction>());
        }
        prize.SetActive(true);
        doorAnimator.SetTrigger("Open Door");
        StartCoroutine(VictoryCoroutine());
        StartCoroutine(BlinkLights());
        base.Victory();

    }

    /// <summary>
    /// Coroutine to be executed when the puzzle is solved
    /// </summary>
    /// <returns>Returns null.</returns>
    private IEnumerator VictoryCoroutine()
    {

        // While the item is active, it keeps rotating
        if (player.PuzzlesDone.HasFlag(myPuzzle) && 
            inventory.Bag.Contains(itemComparer.NoBatteryWalkman) == true)
        {
            Destroy(prize);
        }
        while (prize)
        {
            prize.transform.Rotate(20 * Time.deltaTime, 0, 20 * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }

    /// <summary>
    /// Coroutine responsible for making the lights blink
    /// </summary>
    /// <returns>Returns null.</returns>
    private IEnumerator BlinkLights()
    {

        while (prize)
        {
            for (int i = 0; i < wallLampsParent.Lamps.Length; i++)
            {
                wallLampsParent.Lamps[i].Lights[1].LightComponent.range = 3;
                wallLampsParent.Lamps[i].Lights[1].
                    LightComponent.color = Color.white;

                wallLampsParent.Lamps[i].Lights[0].LightComponent.range = 0;
                yield return new WaitForSeconds(0.1f);
                wallLampsParent.Lamps[i].Lights[1].LightComponent.range = 0f;
            }
        }

        while (!prize)
        {
            for (int i = 0; i < wallLampsParent.Lamps.Length; i++)
            {
                wallLampsParent.Lamps[i].Lights[1].LightComponent.range = 1.54f;
                wallLampsParent.Lamps[i].Lights[0].LightComponent.range = 1.54f;
                yield return null;
            }
        }
    }
}
