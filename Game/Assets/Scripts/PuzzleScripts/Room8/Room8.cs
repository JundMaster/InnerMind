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
    }

    
    public override void Victory()
    {
        doorAnimator.SetTrigger("Open Door");
        StartCoroutine(VictoryCoroutine());
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
}
