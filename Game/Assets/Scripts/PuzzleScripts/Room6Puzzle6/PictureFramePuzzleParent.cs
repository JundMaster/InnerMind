﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for controlling the frames in the Oldportrait Puzzle
/// </summary>
public class PictureFramePuzzleParent : MonoBehaviour
{
    /// <summary>
    /// Frames on the puzzle
    /// </summary>
    [SerializeField]
    private PictureFramePuzzle[] framePictures;

    /// <summary>
    /// Defines wether the puzzle is solved
    /// </summary>
    public bool IsSolved { get; private set; }

    /// <summary>
    /// Start method for PictureFramePuzzleParent
    /// </summary>
    private void Start()
    {
        IsSolved = false;
    }

    /// <summary>
    /// OnEnable method for PictureFramePuzzleParent
    /// </summary>
    private void OnEnable()
    {
        for (int i = 0; i < framePictures.Length; i++)
        {
            framePictures[i].PositionChange += Test;
        }
    }

    private void Test()
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
            IsSolved = true;
            Debug.Log("this shit is solved bruh");
        }
        
    }
}