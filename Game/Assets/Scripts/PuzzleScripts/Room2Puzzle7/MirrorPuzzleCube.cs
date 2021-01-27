using System;
using UnityEngine;

/// <summary>
/// Class responsible for each cube in MirrorPuzzleCube.
/// Extends InteractionCommon
/// </summary>
public class MirrorPuzzleCube : InteractionCommon
{
    // Current position of this cube
    [SerializeField] private LeftMiddleRight pushTo;

    /// <summary>
    /// This method determines the action of the object when clicked
    /// </summary>
    public override void Execute()
    {
        OnCubePosition(pushTo);
    }

    /// <summary>
    /// Method that invokes CubePosition event
    /// </summary>
    /// <param name="cubePos"></param>
    protected virtual void OnCubePosition(LeftMiddleRight pushTo)
        => CubePosition?.Invoke(pushTo);

    /// <summary>
    /// CubePosition event with cube's current position information
    /// </summary>
    public event Action<LeftMiddleRight> CubePosition;

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        return $"Push {pushTo}";
    }
}
