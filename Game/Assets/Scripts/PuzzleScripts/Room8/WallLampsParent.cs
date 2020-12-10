using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLampsParent : MonoBehaviour
{
    [SerializeField]
    private WallLamp[] lamps;

    private YieldInstruction waitForSecs;
    private bool onChainRotation;

    private void OnEnable()
    {
        for (int i = 0; i < lamps.Length; i ++)
        {
            lamps[i].LampRotated += ChainRotation;
        }
    }

    private void Start()
    {
        onChainRotation = false;

        waitForSecs = new WaitForSeconds(0.5f);
    }

    private void ChainRotation(int index)
    {
        StartCoroutine(ChainRotationCoroutine(index - 1));
    }

    private IEnumerator ChainRotationCoroutine(int index)
    {
        for (int i = index - 1; i >= 0; i--)
        {
            yield return waitForSecs;
            Debug.Log($"Lamp[{i}] index: {lamps[i].LampIndex}");
            lamps[i].interactionController.TestExecute();
        }
    }
}
