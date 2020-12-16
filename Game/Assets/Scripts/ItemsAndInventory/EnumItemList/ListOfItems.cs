using System;

/// <summary>
/// List of interectable items ingame
/// </summary>
[Flags]
public enum ListOfItems
{
    Null = 0,
    FamilyPhotograph = 1 << 0,
    PianoKey = 1 << 1,
    PianoKey2 = 1 << 2,
    PianoKey3 = 1 << 3,
    NoBatteryFlashlight = 1 << 4,
    OldBattery = 1 << 5,
    NotRewoundAudioTape = 1 << 6,
    NoBatteryWalkman = 1 << 7,
    Pen = 1 << 8,
    CabinetKey = 1 << 9,
    Map = 1 << 10,
    WalkmanBatteries = 1 << 11,
    PillBottle = 1 << 12,
    Flashlight = OldBattery | NoBatteryFlashlight,
    WalkmanWithoutTape = WalkmanBatteries | NoBatteryWalkman,
    AudioTape = NotRewoundAudioTape | Pen,
    Walkman = AudioTape | WalkmanWithoutTape
}
