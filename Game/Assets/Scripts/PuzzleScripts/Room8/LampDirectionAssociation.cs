using System;
[Serializable]
public struct LampDirectionAssociation
{
    public WallLamp lamp;
    public WallLampDirection direction;
        
    public LampDirectionAssociation(WallLamp _lamp , WallLampDirection _direction)
    {
        lamp = _lamp;
        direction = _direction;
    }
}