﻿using System;
using System.Collections;
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
            //framePictures[i].FrameAligned += SolvedCheck;
            framePictures[i].FrameChanged += ChainTranslation;
        }
    }

    private void ChainTranslation(PictureFramePuzzle pictureFramePuzzle)
    {
        StartCoroutine(ChainTranslationCoroutine(pictureFramePuzzle));
    }

    private IEnumerator ChainTranslationCoroutine(PictureFramePuzzle pictureFramePuzzle)
    {
        if (pictureFramePuzzle.name != "RedFrame")
        {
            for (int i = 0; i < pictureFramePuzzle.LinkedFrames.Length; i++)
            {
                yield return new WaitForSeconds(0.5f);
                IEnumerator chainTranslation = pictureFramePuzzle.LinkedFrames[i].InteractionController.
                                                ChainTranslationExecute(i + 1);
                StartCoroutine(chainTranslation);
            }
        }
        yield return new WaitForSeconds(0.5f);
        SolvedCheck(pictureFramePuzzle);
        //CheckTest(pictureFramePuzzle);
        yield break;
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
            //framePictures[i].FrameAligned -= SolvedCheck;
            framePictures[i].FrameChanged -= ChainTranslation;
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
    private void SolvedCheck(PictureFramePuzzle pictureFramePuzzle)
    {
        Debug.Log("entrou?");
        int solvedCount = 0;
        for (int i = 0; i < framePictures.Length; i++)
        {
            if (framePictures[i].IsFrameAligned())
            {
                solvedCount++;
            }
        }
        if (solvedCount == framePictures.Length)
        {
            Debug.Log("isto quer dizer que resolveu");
            OnPuzzleSolved();
            IsSolved = true;
        }
    }
}
