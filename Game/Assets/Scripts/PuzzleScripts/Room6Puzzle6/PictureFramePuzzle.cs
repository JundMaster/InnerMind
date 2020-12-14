using System;
using UnityEngine;

/// <summary>
/// Responsible for each frame in the puzzle
/// </summary>
public class PictureFramePuzzle : MonoBehaviour
{
    #region Suport Variables

    [SerializeField]
    private Transform[] movePoints;

    [SerializeField]
    private Transform initialPoint;

    [SerializeField]
    private FramePosition solutionPosition;
    [SerializeField]
    private FramePosition initialPosition;
    [SerializeField]
    private bool flipedSolution;

    private bool isFrameFliped;
    private FramePosition currentPosition;
    #endregion


    public Transform InitialPoint
    {
        get => initialPoint;
        set
        {
            initialPoint = value;
        }
    }


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
    
    /// <summary>
    /// Initial position of the frame
    /// </summary>
    public FramePosition InitialPosition
    {
        get => initialPosition;
        private set
        {
            initialPosition = value;
        }
    }
    
    /// <summary>
    /// Current position of the frame
    /// </summary>
    public FramePosition CurrentPosition
    {
        get => currentPosition;
        set
        {
            currentPosition = value;
            OnFrameChange();               
        }
    }

    /// <summary>
    /// Definition of what state - fliped or unfliped - the frame must be
    /// to solve the puzzle
    /// </summary>
    public bool FlipedSolution
    {
        get => flipedSolution;
        private set
        {
            flipedSolution = value;
        }
    }
    /// <summary>
    /// Defines whether the frame is fliped
    /// </summary>
    public bool IsFrameFliped
    {
        get => isFrameFliped;
        set
        {
            if (isFrameFliped != value)
            {
                isFrameFliped = value;
                OnFrameChange();
            }
        }
    }

    /// <summary>
    /// Event fired when the position of the frame changes
    /// </summary>
    public event Action FrameChanged;

    /// <summary>
    /// Start method for PictureFramePuzzle
    /// </summary>
    private void Start()
    {
        InitialPoint = initialPoint;
        IsSolved = false;
        CurrentPosition = initialPosition;      
    }

    /// <summary>
    /// Invokes the <cref>PositionChange</cref> event
    /// </summary>
    /// <param name="currentPositon">Current position of the frame.</param>
    /// <param name="solutionPosition">Position in wich the frame must be in 
    /// order to complete the puzzle </param>
    private void OnFrameChange()
    {
        ValidateSolution();
        FrameChanged?.Invoke();
    }

    /// <summary>
    /// Validates whether the frame is correct positioned
    /// </summary>
    private void ValidateSolution()
    {
        IsSolved =  (CurrentPosition == SolutionPosition) && 
                    (IsFrameFliped == FlipedSolution);
    }

}
