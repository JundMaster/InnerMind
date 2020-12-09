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

    // The actual frame
    private PictureFramePuzzle frame;

    // Defines wether the interaction is still running
    private bool onInteraction;

    // Defines wether the frame can be moved
    private bool canMove;


    /// <summary>
    /// Start method for TranslateInteractionPictureFrame
    /// </summary>
    private void Start()
    {
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
        CanMove();

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && onInteraction || canMove == false)
            yield break;

        // Point to wich the frame will be moved
        Vector3 desiredPoint = transform.parent.position + new Vector3(directionModifier, 0, 0);


        onInteraction = true;
        while (elapsedTime < timeLimit)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position,
                                                            desiredPoint,
                                                            elapsedTime * 0.1f);
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
        else
        {
            canMove = true;
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
        if (directionModifier == 1)
            moveTo = "Move to the left";
        else if (directionModifier == -1)
            moveTo = "Move to the right";
        return moveTo;
    }
}