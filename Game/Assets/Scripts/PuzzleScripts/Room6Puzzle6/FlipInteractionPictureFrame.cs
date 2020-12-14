using System.Collections;
using UnityEngine;
/// <summary>
/// Responsible for the flipping of the frame
/// </summary>
public class FlipInteractionPictureFrame : InteractionCR
{
    private bool onInteraction;
    private bool canFlip;
    [SerializeField]
    private ThoughtHandler thoughtHandler;
    // The actual frame
    private PictureFramePuzzle frame;

    private PictureFramePuzzleParent pictureFramePuzzleParent;

    /// <summary>
    /// Start method for FlipInteractionPictureFrame
    /// </summary>
    private void Start()
    {
        pictureFramePuzzleParent = 
                                FindObjectOfType<PictureFramePuzzleParent>();
        frame = GetComponentInParent<PictureFramePuzzle>();
        onInteraction = false;
        canFlip = false;
    }

    /// <summary>
    /// Responsible for executing the coroutine action.
    /// </summary>
    /// <returns>Returns null.</returns>
    public override IEnumerator CoroutineExecute()
    {
        StartCoroutine(RotationAnimation());
        yield break;
    }

    /// <summary>
    /// Concrete action that is executed when the frame is interacted with.
    /// </summary>
    /// <returns>Returns null.</returns>
    private IEnumerator RotationAnimation()
    {
        float elapsedTime = 0f;
        float timeLimit = 0.5f;
        Quaternion from = transform.rotation;
        Quaternion to = new Quaternion();
        if (!frame.IsFrameFliped)
            to = Quaternion.LookRotation(transform.parent.up,
                                         -transform.parent.forward);
        if (frame.IsFrameFliped)
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

        if (frame.IsFrameFliped) frame.IsFrameFliped = false;
        else if (!frame.IsFrameFliped) frame.IsFrameFliped = true;
    }

    /// <summary>
    /// Defines whether the frame can be flipped
    /// </summary>
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
            thoughtHandler.ExecuteThought(2);
            canFlip = false;
        }
        else
        {
            canFlip = true;
        }
    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this npc
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        return $"Flip frame";
    }
}