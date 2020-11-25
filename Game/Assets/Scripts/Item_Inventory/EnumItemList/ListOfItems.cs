using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum ListOfItems
{
    Null = 0,
    Family_Photograph = 1,
    Piano_Key = 2,
    No_Battery_Lantern = 4,
    Old_Battery = 8,
    Not_Rewinded_Audio_Tape = 16,
    No_Battery_Walkman = 32,
    Pen = 64,
    CabinetKey = 128,
    Map = 256,
    Lantern = Old_Battery | No_Battery_Lantern,
    Walkman = Old_Battery | No_Battery_Walkman,
    Audio_Tape = Not_Rewinded_Audio_Tape | Pen,
}
