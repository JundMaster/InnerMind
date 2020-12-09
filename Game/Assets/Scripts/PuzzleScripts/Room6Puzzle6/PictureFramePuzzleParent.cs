using System;
using UnityEngine;

/// <summary>
/// Responsible for controlling the frames in the Oldportrait Puzzle
/// </summary>
public class PictureFramePuzzleParent : MonoBehaviour
{

    [SerializeField]
    private PictureFramePuzzle[] framePictures;

    /// <summary>
    /// Frames on the puzzle
    /// </summary>
    public PictureFramePuzzle[] FramePictures
    {
        get => framePictures;
        set
        {
            framePictures = value;
        }
    }

    /// <summary>
    /// Defines whether the puzzle is solved
    /// </summary>
    public bool IsSolved { get; private set; }

    /// <summary>
    /// Event fired when the puzzle is solved
    /// </summary>
    public event Action PuzzleSolved;

    #region Unity functions
    /// <summary>
    /// OnEnable method for PictureFramePuzzleParent
    /// </summary>
    private void OnEnable()
    {
        for (int i = 0; i < framePictures.Length; i++)
        {
            framePictures[i].FrameChanged += SolvedCheck;
        }
    }
    /// <summary>
    /// Start method for PictureFramePuzzleParent
    /// </summary>
    private void Start()
    {
        FramePictures = framePictures;
        IsSolved = false;
    }
    /// <summary>
    /// OnDisable method for PictureFramePuzzleParent
    /// </summary>
    private void OnDisable()
    {
        for (int i = 0; i < framePictures.Length; i++)
        {
            framePictures[i].FrameChanged -= SolvedCheck;
        }
    }
    #endregion

    /// <summary>
    /// Invokes the <see cref="PuzzleSolved"/> event
    /// </summary>
    private void OnPuzzleSolved()
    {
        PuzzleSolved?.Invoke();
    }

    /// <summary>
    /// Checks if frames are all marked as solved
    /// </summary>
    private void SolvedCheck()
    {
        int solvedCount = 0;
        for (int i = 0; i < framePictures.Length; i++)
        {
            if (framePictures[i].IsSolved)
            {
                solvedCount++;
            }
        }
        if (solvedCount == framePictures.Length)
        {
            OnPuzzleSolved();
            IsSolved = true;
        }
        
    }
}
