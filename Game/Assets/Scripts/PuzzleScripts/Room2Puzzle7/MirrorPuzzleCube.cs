using UnityEngine;
using System;

/// <summary>
/// Class responsible for each cube in MirrorPuzzleCube.
/// Extends InteractionCommon
/// </summary>
sealed public class MirrorPuzzleCube : InteractionCommon
{
    // Current position of this cube
    [SerializeField] private LeftMiddleRight cubePos;

    /// <summary>
    /// This method determines the action of the object when clicked
    /// </summary>
    public override void Execute()
    {
        CubePosition?.Invoke(cubePos);
    }

    // CubePosition event with cube's current position information
    public event Action<LeftMiddleRight> CubePosition;

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        return $"Move cube {cubePos}";
    }
}
