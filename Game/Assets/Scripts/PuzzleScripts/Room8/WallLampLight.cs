using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLampLight : MonoBehaviour
{
    [SerializeField] 
    private WallLampDirection direction;
    private WallLampDirection currentDirection;

    public WallLampDirection Direction { get; private set; } 

    public WallLampDirection CurrentDirection
    {
        get => currentDirection;
        set
        {
            currentDirection = value;
            if (currentDirection > WallLampDirection.Right)
            {
                currentDirection = WallLampDirection.Top;
            }
        }
    }

    private void Start()
    {
        Direction = direction;
        CurrentDirection = direction;
    }
}
