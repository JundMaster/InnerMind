using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for containing the points in which the 
/// <see cref="PictureFramePuzzle"/> objects will move
/// </summary>
public class FramePointParent : MonoBehaviour
{
    [SerializeField]
    private Transform[] framePoints;
    /// <summary>
    /// Points in which the <see cref="PictureFramePuzzle"/> objects will move
    /// </summary>
    public Transform[] FramePoints => framePoints;
}
