using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for the translation of the picture frame
/// </summary>
public class TranslateInteractionPictureFrame : InteractionCR
{
    // Direction to which the frame will translate to
    [SerializeField]
    private int directionModifier;

    // Modifier for the translation speed of the frame
    [SerializeField]
    [Range(0.01f, 0.5f)]
    private float translationSpeedModifier = 0.03f;

    // The actual frame 
    private PictureFramePuzzle frame;

    // Object that hold all the frame points
    private FramePointParent framePointParent;

    private PictureFramePuzzleParent pictureFramePuzzleParent;

    // Defines whether the interaction is still running
    private bool onInteraction;

    // Defines whether the frame can be moved
    private bool canMove;

    private FramePosition framePointIndex;


    /// <summary>
    /// Start method for TranslateInteractionPictureFrame
    /// </summary>
    private void Start()
    {
        pictureFramePuzzleParent = FindObjectOfType<PictureFramePuzzleParent>();
        framePointParent = FindObjectOfType<FramePointParent>();
        frame = GetComponentInParent<PictureFramePuzzle>();
        onInteraction = false;
        canMove = false;
    }

    /// <summary>
    /// Responsible for executing the coroutine action.
    /// </summary>
    /// <returns>Returns null.</returns>
    public override IEnumerator CoroutineExecute()
    {
        StartCoroutine(TranslateInteraction());
        yield break;
    }

    /// <summary>
    /// Concrete action that is executed when the frame is interacted with.
    /// </summary>
    /// <returns>Returns null.</returns>
    private IEnumerator TranslateInteraction()
    {
        float elapsedTime = 0f;
        float timeLimit = 0.5f;

        framePointIndex = frame.CurrentPosition + 
                                        directionModifier;

        if (framePointIndex > FramePosition.Right)
        {
            framePointIndex = FramePosition.Right;
        }
        else if (framePointIndex < FramePosition.Left)
        {
            framePointIndex = FramePosition.Left;
        }

        CanMove();

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && onInteraction || canMove == false)
            yield break;

        Vector3 framePointPos = framePointParent.
                                FramePoints[(int)framePointIndex].
                                transform.position;

        Vector3 desiredPoint =  transform.parent.position - 
                                (transform.parent.position - framePointPos);

        desiredPoint = new Vector3(desiredPoint.x,
                                   transform.parent.position.y,
                                   transform.parent.position.z);

        onInteraction = true;
        while (elapsedTime < timeLimit)
        {
            transform.parent.position =
                                Vector3.MoveTowards(
                                    transform.parent.position,
                                    desiredPoint,
                                    elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Changes the CurrentPosition of the frame
        frame.CurrentPosition += directionModifier;
        onInteraction = false;

    }

    /// <summary>
    /// Validates wheater the action is possible.
    /// </summary>
    private void CanMove()
    {
        
        if (frame.CurrentPosition == FramePosition.Right &&
            directionModifier == 1)
        {
            canMove = false;
        }
        else if (frame.CurrentPosition == FramePosition.Left &&
                directionModifier == -1)
        {
            canMove = false;
        }
        else if (frame.IsFrameFliped)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        foreach(PictureFramePuzzle f in pictureFramePuzzleParent.FramePictures)
        {
            if (f.IsFrameFliped && 
                f.CurrentPosition == frame.CurrentPosition + directionModifier)
            {
                canMove = false;
            }
        }
    }


    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this npc
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        string moveTo = "";
        if (frame.IsFrameFliped)
        {
            return "Unflip before you move";
        }
        if (directionModifier == 1)
            moveTo = "Move to the left";
        else if (directionModifier == -1)
            moveTo = "Move to the right";
        return moveTo;
    }
}