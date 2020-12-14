using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for the lights associated to the <see cref="WallLamp"/> objects
/// </summary>
public class WallLampLight : MonoBehaviour
{
    [SerializeField] 
    private WallLampDirection direction;
    private WallLampDirection currentDirection;

    /// <summary>
    /// Light component of this object
    /// </summary>
    public Light LightComponent { get; private set; }

    /// <summary>
    /// Defines whether the light is on the Top or the Bottom of the
    /// <see cref="WallLamp"/> object
    /// </summary>
    public WallLampDirection Direction { get; private set; } 

    /// <summary>
    /// Direction to which the <see cref="WallLampLight"/> is pointing
    /// </summary>
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

    /// <summary>
    /// Start method for WallLampLight
    /// </summary>
    private void Start()
    {
        Direction = direction;
        CurrentDirection = direction;
        LightComponent = GetComponent<Light>();
    }
}
