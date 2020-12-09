using System.Collections;
using UnityEngine;

public class FlipInteractionPictureFrame : InteractionCR
{
    private bool onInteraction;

    // The actual frame
    private PictureFramePuzzle frame;

    private void Start()
    {
        frame = GetComponentInParent<PictureFramePuzzle>();
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
        Quaternion to = new Quaternion();
        if (!frame.CurrentFlipState)
            to = Quaternion.LookRotation(transform.parent.up, -transform.parent.forward);
        if (frame.CurrentFlipState)
            to = Quaternion.LookRotation(-transform.parent.up, transform.parent.forward);


        if (elapsedTime < timeLimit && onInteraction)
            yield break;

        onInteraction = true;
        while (elapsedTime < timeLimit)
        {
            transform.parent.rotation = Quaternion.Slerp(from, to, elapsedTime * 2.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        onInteraction = false;

        if (frame.CurrentFlipState) frame.CurrentFlipState = false;
        else if (!frame.CurrentFlipState) frame.CurrentFlipState = true;
    }


    public override string ToString()
    {
        return $"Fliped: {frame.CurrentFlipState}";
    }
}