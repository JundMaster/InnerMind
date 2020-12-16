using System;
using System.Collections;
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
    public TranslateInteractionPictureFrame InteractionController { get; private set; }
    #endregion

    public PictureFramePuzzle[] LinkedFrames { get; private set; }

    /// <summary>
    /// Defintion for whether the frame is well positioned and fliped
    /// </summary>
    public bool IsSolved { get; private set; }

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

    private FramePosition currentPosition;
    /// <summary>
    /// Current position of the frame
    /// </summary>
    public FramePosition CurrentPosition 
    {
        get => currentPosition;
        
        private set
        {
            currentPosition = value;
            //Debug.Log($"{name} CURRENT DAMN POSITION: {value}");
        }
    }


    /// <summary>
    /// Event fired when the position of the frame changes
    /// </summary>
    public event Action<PictureFramePuzzle> FrameChanged;

    public event Action<PictureFramePuzzle> FrameAligned;

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
        IsSolved = false;
    }

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

    private void OnFrameAligned()
    {
        FrameAligned?.Invoke(this);
    }

    public void MoveFrame(FramePosition position)
    {
        CurrentPosition = position;
        if (IsFrameAligned())
        {
            OnFrameAligned();
        }
    }

    public void ChainTranslation(FramePosition position)
    {
        MoveFrame(position);
        OnFrameChange();
    }

    public bool IsFrameAligned() => CurrentPosition == SolutionPosition;

}
