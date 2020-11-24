using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

sealed public class MirrorPuzzleCube : InteractionCommon
{
    [SerializeField] private LeftMiddleRight cubePos;

    public override void Execute()
    {
        CubePosition(cubePos);
    }

    public event Action<LeftMiddleRight> CubePosition;

    public override string ToString()
    {
        return $"Move cube {cubePos}";
    }
}
