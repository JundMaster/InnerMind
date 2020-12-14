﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room8 : PuzzleBase
{
    [SerializeField] private GameObject prize;
    [SerializeField] private ScriptableItem prizeScriptableItem;
    [SerializeField] private WallLampsParent wallLampsParent;
    [SerializeField] private Animator doorAnimator;

    /// <summary>
    /// Property that checks if the puzzle was finished
    /// </summary>
    public bool PuzzleAlreadyDone { get; private set; }

    /// <summary>
    /// OnEnable method for Room8
    /// </summary>
    private void Start()
    {
        wallLampsParent.LampsAligned += Victory;
        prize.SetActive(false);

        //if (player.PuzzlesDone.HasFlag(myPuzzle))
        //{
        //    //prize = null;
        //    Destroy(prize);
        //    Victory();
        //} 
    }


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

    private IEnumerator VictoryCoroutine()
    {

        // While the item is active, it keeps rotating
        if (player.PuzzlesDone.HasFlag(myPuzzle) && 
            inventory.Bag.Contains(prizeScriptableItem) == true)
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