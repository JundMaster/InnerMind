using System;
/// <summary>
/// Defines a association of a <see cref="WallLamp"/> and a side that it will
/// be on
/// </summary>
[Serializable]
public struct LampDirectionAssociation
{
    public WallLamp lamp;
    public WallLampDirection side;

    /// <summary>
    /// Constructor, that creates a new instance of LampDirectionAssociation
    /// and initializes its members
    /// </summary>
    /// <param name="_lamp">Lamp with which the association will be 
    /// made</param>
    /// <param name="side">Side in which the lamp will be</param>
    public LampDirectionAssociation(WallLamp _lamp , WallLampDirection side)
    {
        lamp = _lamp;
        this.side = side;
    }
}