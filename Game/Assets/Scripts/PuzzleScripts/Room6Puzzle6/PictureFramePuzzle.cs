using System;
using UnityEngine;

/// <summary>
/// Responsible for each frame in the puzzle
/// </summary>
public class PictureFramePuzzle : MonoBehaviour
{
    #region Suport Variables
    [SerializeField]
    private FramePosition solutionPosition;
    [SerializeField]
    private FramePosition initialPosition;
    [SerializeField]
    private PictureFramePuzzle[] linkedFrames;
    private FramePosition currentPosition;
    #endregion
    
    /// <summary>
    /// Interaction controller of the frame 
    /// </summary>
    public TranslateInteractionPictureFrame InteractionController 
    { get; private set; }

    /// <summary>
    /// Frames that are attatched to this frame
    /// </summary>
    public PictureFramePuzzle[] LinkedFrames { get; private set; }

    /// <summary>
    /// Position in wich the frame must be in order to complete the puzzle
    /// </summary>
    public FramePosition SolutionPosition
    {
        get => solutionPosition;
        private set
        {
            solutionPosition = value;
        }
    }

    /// <summary>
    /// Current position of the frame
    /// </summary>
    public FramePosition CurrentPosition 
    {
        get => currentPosition;
        
        private set
        {
            currentPosition = value;
        }
    }

    /// <summary>
    /// Event fired when the position of the frame changes
    /// </summary>
    public event Action<PictureFramePuzzle> FrameChanged;

    /// <summary>
    /// Event fired when the frame is aligned
    /// </summary>
    public event Action<PictureFramePuzzle> FrameAligned;

    #region Unity Function
    /// <summary>
    /// Awake method for PictureFramePuzzle
    /// </summary>
    private void Awake()
    {
        LinkedFrames = linkedFrames;
        CurrentPosition = initialPosition;
    }
    /// <summary>
    /// Start method for PictureFramePuzzle
    /// </summary>
    private void Start()
    {
        InteractionController = GetComponentInChildren<TranslateInteractionPictureFrame>();
    }
    #endregion

    /// <summary>
    /// Invokes the <cref>PositionChange</cref> event
    /// </summary>
    /// <param name="currentPositon">Current position of the frame.</param>
    /// <param name="solutionPosition">Position in wich the frame must be in 
    /// order to complete the puzzle </param>
    private void OnFrameChange()
    {
        FrameChanged?.Invoke(this);

        if (IsFrameAligned())
        {
            OnFrameAligned();
        }
    }

    /// <summary>
    /// Fires the <see cref="FrameAligned"/> event
    /// </summary>
    private void OnFrameAligned()
    {
        FrameAligned?.Invoke(this);
    }

    /// <summary>
    /// Changed the position of the frame and fires the 
    /// <see cref="FrameAligned"/> event
    /// </summary>
    /// <param name="position">Position to which the frame will move</param>
    public void MoveFrame(FramePosition position)
    {
        CurrentPosition = position;
        if (IsFrameAligned())
        {
            OnFrameAligned();
        }
    }

    /// <summary>
    /// Executes the chain movement of the frames and fires the 
    /// <see cref="FrameChanged"/> event
    /// </summary>
    /// <param name="position">Position to which the frame will move</param>
    public void ChainTranslation(FramePosition position)
    {
        MoveFrame(position);
        OnFrameChange();
    }

    /// <summary>
    /// Determines whether the frame is in the right position
    /// </summary>
    /// <returns></returns>
    public bool IsFrameAligned() => CurrentPosition == SolutionPosition;

}
