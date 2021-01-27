using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for the interactions with the frame.
/// </summary>
public class FrameTranslationInteraction : InteractionCommon
{
    private bool onInteraction;
    private FramePointParent framePointParent;
    private Frame frame;
    private Coroutine moveFrame;
    private int frameCount;
    private bool executeChainReaction;

    /// <summary>
    /// Reference of the frame that is being interacted with.
    /// </summary>
    public Frame Frame => frame;

    /// <summary>
    /// Get or sets whether it is possible to interact with the frame.
    /// </summary>
    public bool CanInteract { get; set; }

    /// <summary>
    /// Start method for FrameTranslationInteraction.
    /// </summary>
    private void Start()
    {
        onInteraction = false;
        CanInteract = true;
        framePointParent = FindObjectOfType<FramePointParent>();
        frame = GetComponent<Frame>();
        frameCount = 0;
        executeChainReaction = true;
    }

    /// <summary>
    /// Determines the action of the frame when the player interacts with it.
    /// </summary>
    public override void Execute()
    {
        if (moveFrame == null && CanInteract)
        {
            executeChainReaction = true;
            moveFrame = StartCoroutine(Translation(Frame, 1));
        }
    }

    /// <summary>
    /// Responisble for moving the given frame to its next position based on
    /// the given mode modifier.
    /// </summary>
    /// <param name="frameToMove">Frame to be moved.</param>
    /// <param name="moveModifier">Modifier that determines how many position
    /// the frame will move.</param>
    /// <returns></returns>
    private IEnumerator Translation(
            Frame frameToMove, 
            int moveModifier)
    {
        float elapsedTime = 0f;
        float timeLimit = 0.25f;

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && onInteraction)
            yield break;

        // Disables the possibility of interaction with the other frames while
        // the resulting interactions of this frame is happening
        for (int i = 0; i < Frame.LinkedFrames.Length; i++)
        {
            Frame.LinkedFrames[i].GetComponent<FrameTranslationInteraction>().
                CanInteract = false;
        }

        // Point to which the frame will move
        Vector3 desiredPoint;

        // Position that the frame will be after being moved
        FramePosition nextPosition =
            GetNextFramePosition(frameToMove.CurrentPosition, moveModifier);

        // Defines the exact position of the desired point
        switch (nextPosition)
        {
            case FramePosition.Right:
                desiredPoint =
                    frameToMove.transform.position -
                    (frameToMove.transform.position - 
                        framePointParent.FramePoints[0].position);
                break;           
            case FramePosition.Mid:
                desiredPoint =
                    frameToMove.transform.position -
                    (frameToMove.transform.position -
                        framePointParent.FramePoints[1].position);
                break;    
            case FramePosition.Left:
                desiredPoint =
                    frameToMove.transform.position -
                    (frameToMove.transform.position -
                        framePointParent.FramePoints[2].position);
                break;
            default:
                desiredPoint =
                    frameToMove.transform.position -
                    (frameToMove.transform.position -
                        framePointParent.FramePoints[0].position);
                break;
        }

        // Changes the Z and Y coordenates of the desiredPoint so it only moves
        // on the X axis.
        desiredPoint.z = frameToMove.transform.position.z;
        desiredPoint.y = frameToMove.transform.position.y;
        onInteraction = true;
        SoundManager.PlaySound(SoundClip.WoodDragging);
        while (elapsedTime < timeLimit)
        {
            // Moves the frame
            frameToMove.transform.position = Vector3.MoveTowards(
                                            frameToMove.transform.position,
                                            desiredPoint,
                                            Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        onInteraction = false;

        // Changes the position of the frame
        switch(frameToMove.CurrentPosition)
        {
            case FramePosition.Right:
                frameToMove.CurrentPosition = FramePosition.Mid;
                break;  
            case FramePosition.Mid:
                frameToMove.CurrentPosition = FramePosition.Left;
                break;           
            case FramePosition.Left:
                frameToMove.CurrentPosition = FramePosition.Right;
                break;
        }
        frameToMove.CurrentPosition = nextPosition;
        moveFrame = null;

        // Moves the frames linked to this one
        if (executeChainReaction)
        {
            frameCount++;
            if (frameCount == 1)
            {
                moveFrame = 
                        StartCoroutine(Translation(Frame.LinkedFrames[0], 2));
                yield break;
            }
            else if (frameCount == 2)
            {
                moveFrame = 
                        StartCoroutine(Translation(Frame.LinkedFrames[1], 1));
                frameCount = 0;
                executeChainReaction = false;
                yield break;
            }
        }

        // Enables the possibility of interaction with the other frames
        for (int i = 0; i < Frame.LinkedFrames.Length; i++)
        {
            Frame.LinkedFrames[i].GetComponent<FrameTranslationInteraction>().
                CanInteract = true;
        }

        // Fires the FramesChanged event
        OnFramesChanged();
    }   

    /// <summary>
    /// Move the given frame to the solution point.
    /// </summary>
    /// <param name="frameToMove">Frame to be moved.</param>
    public void MoveToSolutionPoint(Frame frameToMove)
    {
        Vector3 desiredPoint;
        desiredPoint =
            frameToMove.transform.position -
            (frameToMove.transform.position -
                framePointParent.FramePoints[2].position);

        desiredPoint.z = frameToMove.transform.position.z;
        desiredPoint.y = frameToMove.transform.position.y;
        frameToMove.transform.position = desiredPoint;
    }

    /// <summary>
    /// Invokes the <see cref="FramesChanged"/> event.
    /// </summary>
    protected virtual void OnFramesChanged()
    {
        FramesChanged?.Invoke();
    }

    /// <summary>
    /// Gets the next position to which the frame will move.
    /// </summary>
    /// <param name="position">Current position of the frame.</param>
    /// <param name="moveModifier">Modifier that determines how many position
    /// the frame will move.</param>
    /// <returns></returns>
    private FramePosition GetNextFramePosition(FramePosition position, 
                                            int moveModifier)
    {
        
        if (moveModifier == 1)
        {
            if (position == FramePosition.Mid)
                return FramePosition.Left;

            if (position == FramePosition.Left)
                return FramePosition.Right;

            else
                return FramePosition.Mid;
        }
        else
        {
            if (position == FramePosition.Mid)
                return FramePosition.Right;

            if (position == FramePosition.Left)
                return FramePosition.Mid;

            else
                return FramePosition.Left;
        }

    }

    /// <summary>
    /// Returns the text to be shown when the player hovers the crosshair over
    /// the frame.
    /// </summary>
    /// <returns>The text return by ToString()</returns>
    public override string ToString()
    {
        return "Move Frame";
    }

    /// <summary>
    /// Event that is fired when the player interacts with the frame.
    /// </summary>
    /// <remarks>
    /// This event is fired so the <see cref="Room6"/> makes the verification
    /// of the solution.
    /// </remarks>
    public event Action FramesChanged;
}