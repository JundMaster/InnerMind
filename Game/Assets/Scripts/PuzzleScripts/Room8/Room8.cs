using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room8 : PuzzleBase
{
    [SerializeField] private GameObject prize;
    [SerializeField] private ScriptableItem prizeScriptableItem;
    [SerializeField] private WallLampsParent wallLampsParent;
    [SerializeField] private Animator doorAnimator;

    /// <summary>
    /// OnEnable method for Room8
    /// </summary>
    private void Start()
    {
        wallLampsParent.LampsAligned += Victory;
        prize.SetActive(false);
    }


    public override void Victory()
    {
        prize.SetActive(true);
        doorAnimator.SetTrigger("Open Door");
        StartCoroutine(VictoryCoroutine());
        StartCoroutine(BlinkLights());
        base.Victory();

    }

    private IEnumerator VictoryCoroutine()
    {

        // While the item is active, it keeps rotating
        while (prize)
        {
            prize.transform.Rotate(20 * Time.deltaTime, 0, 20 * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }

    private IEnumerator BlinkLights()
    {

        while (prize)
        {
            for (int i = 0; i < wallLampsParent.Lamps.Length; i++)
            {
                wallLampsParent.Lamps[i].Lights[1].LightComponent.range = 3;
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
