﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for controlling all the <see cref="WallLamp"/> objects
/// on the scene
/// </summary>
public class WallLampsParent : MonoBehaviour
{
    // List of WallLamp objects that will be under the controll of
    // WallLampsParent
    [SerializeField]
    private WallLamp[] lamps;

    // Seconds the WallLampsParent should wait before rotating the lamps that
    // will be affected by the chain rotation
    private YieldInstruction waitForSecs;

    /// <summary>
    /// Event that fires when all the frames are aligned
    /// </summary>
    public event Action LampsAligned;

    /// <summary>
    /// Collection of <see cref="WallLamp"/> objects being controlled
    /// </summary>
    public WallLamp[] Lamps { get; private set; }

    #region Unity funcions
    /// <summary>
    /// OnEnable method for WallLampsParent
    /// </summary>
    private void OnEnable()
    {
        for (int i = 0; i < lamps.Length; i ++)
        {
            lamps[i].LampRotated += ChainRotation;
            lamps[i].LampAligned += CheckLampsAlignmed;
        }
    }
    /// <summary>
    /// Start method for WallLampsParent
    /// </summary>
    private void Start()
    {
        Lamps = lamps;
        waitForSecs = new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// OnDisable method for WallLampsParent
    /// </summary>
    private void OnDisable()
    {
        for (int i = 0; i < lamps.Length; i++)
        {
            lamps[i].LampRotated -= ChainRotation;
            lamps[i].LampAligned -= CheckLampsAlignmed;
        }
    }
    #endregion

    /// <summary>
    /// Invokes the <see cref="LampsAligned"/> event
    /// </summary>
    private void OnLampsAligned()
    {
        LampsAligned?.Invoke();
    }

    /// <summary>
    /// Checks the alignment of all wall lamps
    /// </summary>
    private void CheckLampsAlignmed()
    {
        byte numOfLampsAligned = 0;
        for (int i = 0; i < Lamps.Length; i ++)
        {
            if (Lamps[i].IsAligned())
            {
                numOfLampsAligned++;
            }
        }
        if (numOfLampsAligned == Lamps.Length)
        {
            OnLampsAligned();
        }
    }

    /// <summary>
    /// Starts the coroutine responsible for rotating the lamps as a chain
    /// reaction
    /// </summary>
    /// <param name="index">Index of the <see cref="WallLamp"/> that is
    /// being rotated and will rotate the other in consequence</param>
    private void ChainRotation(int index)
    {
        StartCoroutine(ChainRotationCoroutine(index - 1));
    }

    /// <summary>
    /// Coroutine that will rotate the lamps in chain reaction
    /// </summary>
    /// <param name="index">Index of the <see cref="WallLamp"/> that is
    /// being rotated and will rotate the other in consequence</param>
    /// <returns>Returns null</returns>
    private IEnumerator ChainRotationCoroutine(int index)
    {
        for (int i = index - 1; i >= 0; i--)
        {
            yield return waitForSecs;
            IEnumerator chainRotation = Lamps[i].
                                        InteractionController.
                                        ChainRotationExecute();
            StartCoroutine(chainRotation);
        }
    }
}
