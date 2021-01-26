﻿using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for controlling the frames in the Oldportrait Puzzle.
/// </summary>
public class PictureFramePuzzleParent : MonoBehaviour
{
    [SerializeField]
    private PictureFramePuzzle[] framePictures;

    private Room6 room6;

    /// <summary>
    /// Frames on the puzzle.
    /// </summary>
    public PictureFramePuzzle[] FramePictures
    {
        get => framePictures;
    }

    /// <summary>
    /// Event fired when the puzzle is solved.
    /// </summary>
    public event Action PuzzleSolved;

    #region Unity functions
    /// <summary>
    /// OnEnable method for PictureFramePuzzleParent
    /// </summary>
    private void OnEnable()
    {
        for (int i = 0; i < FramePictures.Length; i++)
        {
            FramePictures[i].FrameChanged += ChainTranslation;
        }
    }

    /// <summary>
    /// Start method for PictureFramePuzzleParent
    /// </summary>
    private void Start()
    {
        room6 = FindObjectOfType<Room6>();
    }

    /// <summary>
    /// OnDisable method for PictureFramePuzzleParent
    /// </summary>
    private void OnDisable()
    {
        for (int i = 0; i < FramePictures.Length; i++)
        {
            FramePictures[i].FrameChanged -= ChainTranslation;
        }
    }
    #endregion


    /// <summary>
    /// Executes the coroutine that moves the given frame in chain reaction
    /// </summary>
    /// <param name="pictureFramePuzzle">Frame to be moved</param>
    private void ChainTranslation(PictureFramePuzzle pictureFramePuzzle)
    {   
        StartCoroutine(ChainTranslationCoroutine(pictureFramePuzzle));
    }

    /// <summary>
    /// Coroutine that moves the given frame in chain reaction
    /// </summary>
    /// <param name="pictureFramePuzzle">Frame to be moved</param>
    /// <returns></returns>
    private IEnumerator ChainTranslationCoroutine(
                PictureFramePuzzle pictureFramePuzzle)
    {
        if (pictureFramePuzzle.name != "RedFrame")
        {
            for (int i = 0; i < pictureFramePuzzle.LinkedFrames.Length; i++)
            {
                yield return new WaitForSeconds(0.5f);
                IEnumerator chainTranslation = 
                                                pictureFramePuzzle.
                                                LinkedFrames[i].
                                                InteractionController.
                                                ChainTranslationExecute(i + 1);

                StartCoroutine(chainTranslation);
            }
        }
        yield return new WaitForSeconds(1f);
        SolvedCheck(pictureFramePuzzle);
        //yield return new WaitForSeconds(2f);
        //for (int i = 0; i < FramePictures.Length; i++)
        //{
        //    FramePictures[i].
        //        GetComponentInChildren<TranslateInteractionPictureFrame>().
        //        CanInteract = true;
        //}
        yield break;
    }

    /// <summary>
    /// Checks if frames are all marked as solved
    /// </summary>
    private void SolvedCheck(PictureFramePuzzle pictureFramePuzzle)
    {
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
            room6.Victory();
        }
    }
}
