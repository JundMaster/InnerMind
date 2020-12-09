using System.Collections.Generic;
using UnityEngine;

public class RotationDirection
{
	public static Dictionary<Direction, Vector3> direction = new Dictionary<Direction, Vector3>()
    {
        {Direction.Front, new Vector3(0,0,1) },
        {Direction.Right, new Vector3(-1,0,0) },
        {Direction.Back, new Vector3(0,0,-1) },
        {Direction.Left, new Vector3(1,0,0) }
    };
}