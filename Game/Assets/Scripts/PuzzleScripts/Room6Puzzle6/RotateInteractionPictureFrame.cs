using System.Collections;
using UnityEngine;

public class RotateInteractionPictureFrame : InteractionCR
{
    private bool onInteraction;
    private Transform parentTransform;
    private PictureFramePuzzle frame;
    private void Start()
    {
        parentTransform = GetComponentInParent<Transform>();
        frame = GetComponent<PictureFramePuzzle>();
        onInteraction = false;
    }
    public override IEnumerator CoroutineExecute()
    {
        StartCoroutine(RotationAnimation());
        yield break;
    }

    private IEnumerator RotationAnimation()
    {
        float elapsedTime = 0f;
        float timeLimit = 0.5f;
        Quaternion from = transform.rotation;   
        Quaternion to = Quaternion.LookRotation(-parentTransform.right, parentTransform.up);
        if (elapsedTime < timeLimit && onInteraction)
            yield break;

        onInteraction = true;
        while (elapsedTime < timeLimit)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsedTime * 2.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        onInteraction = false;
    }

    public override string ToString()
    {
        return "Click on that shit nigga! I Dare you!";
    }
}