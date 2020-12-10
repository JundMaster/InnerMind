using System.Collections;
using UnityEngine;

public class WallLampInteraction : InteractionCR
{
    // Defines whether the interaction is still running
    private bool onInteraction;

    private WallLamp lamp;

    private void Start()
    {
        onInteraction = false;
        lamp = GetComponent<WallLamp>();
    }
    public override IEnumerator CoroutineExecute()
    {
        StartCoroutine(RotationInteraction());
        yield break;
    }

    public void TestExecute()
    {
        Debug.Log("on TestExecute");
        StartCoroutine(RotationInteractionCommon());
        //yield break;
    }

    private IEnumerator RotationInteraction()
    {

        float elapsedTime = 0f;
        float timeLimit = 0.5f;
        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.LookRotation(transform.forward, -transform.right);

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && onInteraction)
            yield break;

        onInteraction = true;
        while (elapsedTime < timeLimit)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsedTime*2.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //lamp.RotateLamp();
        lamp.ChainRotation();

        //Debug.Log(lamp.CurrentDirection);
        onInteraction = false;
    } 
    
    private IEnumerator RotationInteractionCommon()
    {

        float elapsedTime = 0f;
        float timeLimit = 0.5f;
        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.LookRotation(transform.forward, -transform.right);

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && onInteraction)
            yield break;

        onInteraction = true;
        while (elapsedTime < timeLimit)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsedTime*2.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //lamp.ChainRotation();
        lamp.RotateLamp();

        //Debug.Log(lamp.CurrentDirection);
        onInteraction = false;
    }

    public override string ToString()
    {
        return "Rotate lamp";
    }
}