﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum ListOfItems
{
    Null = 0,
    FamilyPhotograph = 1 << 0,
    PianoKey = 1 << 1,
    PianoKey2 = 1 << 2,
    PianoKey3 = 1 << 3,
    NoBatteryLantern = 1 << 4,
    OldBattery = 1 << 5,
    NotRewoundAudioTape = 1 << 6,
    NoBatteryWalkman = 1 << 7,
    Pen = 1 << 8,
    CabinetKey = 1 << 9,
    Map = 1 << 10,
    Lantern = OldBattery | NoBatteryLantern,
    Walkman = OldBattery | NoBatteryWalkman,
    AudioTape = NotRewoundAudioTape | Pen,
}
