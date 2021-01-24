using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for the translation of the picture frame
/// </summary>
public class TranslateInteractionPictureFrame : InteractionCR
{
    // Direction to which the frame will translate
    [SerializeField]
    private int directionModifier;

    [SerializeField]
    private ThoughtHandler thoughtHandler;

    // The actual frame 
    private PictureFramePuzzle frame;

    // Object that hold all the frame points
    private FramePointParent framePointParent;

    // Defines whether the interaction is still running
    private bool onInteraction;

    private bool interacting;

    public bool CanInteract { get; set; }

    /// <summary>
    /// Index of the point in which the frame is
    /// </summary>
    private FramePosition FramePositionIndex { get; set; }

    /// <summary>
    /// Start method for TranslateInteractionPictureFrame
    /// </summary>
    private void Start()
    {
        interacting = false;
        CanInteract = true;
        framePointParent = FindObjectOfType<FramePointParent>();
        frame = GetComponentInParent<PictureFramePuzzle>();
        FramePositionIndex = frame.CurrentPosition;
        onInteraction = false;

    }

    /// <summary>
    /// Responsible for executing the coroutine action.
    /// </summary>
    /// <returns>Returns null.</returns>
    public override IEnumerator CoroutineExecute()
    {
        if (CanInteract)
        {
            StartCoroutine(TranslateInteraction());
            SoundManager.PlaySound(SoundClip.WoodDragging);
        }
        yield break;
    }

    /// <summary>
    /// Coroutine that moves the frames in chain reaction
    /// </summary>
    /// <param name="pointModifier">Index of the point to which the
    /// frame will move</param>
    /// <returns>Returns null</returns>
    public IEnumerator ChainTranslationExecute(int pointModifier)
    {
        if (TranslateInteractionCommon(pointModifier) != null)
        {
            StartCoroutine(TranslateInteractionCommon(pointModifier));
            SoundManager.PlaySound(SoundClip.WoodDragging);
        }
        yield break;
    }

    /// <summary>
    /// Coroutine that moves the frame
    /// </summary>
    /// <param name="pointModifier">Index of the point to which the
    /// frame will move</param>
    /// <returns>Returns null</returns>
    private IEnumerator Translate(int pointModifier = 0)
    {
        float elapsedTime = 0f;
        float timeLimit = 0.25f;

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && onInteraction)
            yield break;


        FramePositionIndex += pointModifier;
        //FramePositionIndex++;

        if (FramePositionIndex > FramePosition.Right)
        {
            int newPositionIndex = (int)FramePositionIndex - 3;
            FramePositionIndex = (FramePosition)newPositionIndex;
        }

        Vector3 framePointPos = framePointParent.
                                FramePoints[(int)FramePositionIndex].position;

        Vector3 desiredPoint = transform.parent.position -
                                (transform.parent.position - framePointPos);

        desiredPoint = new Vector3(desiredPoint.x,
                                   transform.parent.position.y,
                                   transform.parent.position.z);

        onInteraction = true;
        while (elapsedTime < timeLimit)
        {
            transform.parent.position = Vector3.MoveTowards(
                                            transform.parent.position,
                                            desiredPoint,
                                            elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        onInteraction = false;
    }

    /// <summary>
    /// Coroutine that simply moves the frame
    /// </summary>
    /// <param name="pointModifier">Index of the point to which the
    /// frame will move</param>
    /// <returns>Returns null</returns>
    private IEnumerator TranslateInteractionCommon(int pointModifier = 0)
    {
        StartCoroutine(Translate(pointModifier));

        float elapsedTime = 0f;
        float timeLimit = 0.5f;

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && interacting)
            yield break;

        interacting = true;
        while (elapsedTime < timeLimit)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        interacting = false;

        frame.MoveFrame(FramePositionIndex);
        yield break;
    }

    /// <summary>
    /// Concrete action that is executed when the frame is interacted with.
    /// </summary>
    /// <returns>Returns null.</returns>
    public IEnumerator TranslateInteraction()
    {
        StartCoroutine(Translate(1));

        float elapsedTime = 0f;
        float timeLimit = 0.5f;

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && interacting)
            yield break;

        interacting = true;
        while (elapsedTime < timeLimit)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        interacting = false;

        frame.ChainTranslation(FramePositionIndex);
        yield break;
    }


    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this frame
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        return "Move frame";
    }
}