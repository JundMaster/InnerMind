using System.Collections;
using UnityEngine;

public class FlipInteractionPictureFrame : InteractionCR
{
    private bool onInteraction;
    private bool canFlip;

    // The actual frame
    private PictureFramePuzzle frame;

    private PictureFramePuzzleParent pictureFramePuzzleParent;

    private void Start()
    {
        pictureFramePuzzleParent = FindObjectOfType<PictureFramePuzzleParent>();
        frame = GetComponentInParent<PictureFramePuzzle>();
        onInteraction = false;
        canFlip = false;
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
            to = Quaternion.LookRotation(transform.parent.up,
                                         -transform.parent.forward);
        if (frame.CurrentFlipState)
            to = Quaternion.LookRotation(-transform.parent.up,
                                         transform.parent.forward);

        CanFlip();
        if (elapsedTime < timeLimit && onInteraction || canFlip == false)
            yield break;

        onInteraction = true;
        while (elapsedTime < timeLimit)                                        
        {
            transform.parent.rotation = Quaternion.Slerp(from,
                                                         to,
                                                         elapsedTime * 2.5f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        onInteraction = false;

        if (frame.CurrentFlipState) frame.CurrentFlipState = false;
        else if (!frame.CurrentFlipState) frame.CurrentFlipState = true;
    }

    private void CanFlip()
    {
        int count = 0;
        for (int i = 0; i < pictureFramePuzzleParent.FramePictures.Length; i++)
        {
            if (pictureFramePuzzleParent.FramePictures[i].CurrentPosition ==
                frame.CurrentPosition)
            {
                count++;
            }
        }
        if (count > 1)
        {
            canFlip = false;
        }
        else
        {
            canFlip = true;
        }
    }
    public override string ToString()
    {
        return $"Fliped: {frame.CurrentFlipState}";
    }
}