using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum ListOfItems
{
    Null = 0,
    Family_Photograph = 1 << 0,
    Piano_Key = 1 << 1,
    No_Battery_Lantern = 1 << 2,
    Old_Battery = 1 << 3,
    Not_Rewinded_Audio_Tape = 1 << 4,
    No_Battery_Walkman = 1 << 5,
    Pen = 1 << 6,
    CabinetKey = 1 << 7,
    Map = 1 << 8,
    Piano_Key2 = 1 << 9,
    Piano_Key3 = 1 << 10,
    Lantern = Old_Battery | No_Battery_Lantern,
    Walkman = Old_Battery | No_Battery_Walkman,
    Audio_Tape = Not_Rewinded_Audio_Tape | Pen,
}
