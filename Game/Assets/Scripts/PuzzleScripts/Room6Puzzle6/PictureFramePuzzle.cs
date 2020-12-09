using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for each frame in the puzzle
/// </summary>
public class PictureFramePuzzle : MonoBehaviour
{ 
    [SerializeField]
    private FramePosition solutionPosition;
    [SerializeField]
    private FramePosition initialPosition;
    [SerializeField]
    private bool flipedSolution;
    [SerializeField]
    private FramePoint framePoint;

    public FramePoint FramePoint
    {
        get => framePoint;
        set
        {
            framePoint = value;
        }
    }

    private bool currentFlipState;

    private FramePosition currentPosition;

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
            OnPositionChange();               
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

    public bool CurrentFlipState
    {
        get => currentFlipState;
        set
        {
            if (currentFlipState != value)
            {
                currentFlipState = value;
                OnPositionChange();
            }
        }
    }

    /// <summary>
    /// Event fired when the position of the frame changes
    /// </summary>
    public event Action PositionChange;

    /// <summary>
    /// Start method for PictureFramePuzzle
    /// </summary>
    private void Start()
    {
        IsSolved = false;
        CurrentPosition = initialPosition;
        FramePoint = framePoint;
    }

    /// <summary>
    /// Invokes the <cref>PositionChange</cref> event
    /// </summary>
    /// <param name="currentPositon">Current position of the frame.</param>
    /// <param name="solutionPosition">Position in wich the frame must be in 
    /// order to complete the puzzle </param>
    private void OnPositionChange()
    {
        ValidateSolution();
        PositionChange?.Invoke();
    }

    /// <summary>
    /// Validates wether the frame is correct positioned
    /// </summary>
    private void ValidateSolution()
    {
        IsSolved =  (CurrentPosition == SolutionPosition) && 
                    (CurrentFlipState == FlipedSolution);

    }

}
