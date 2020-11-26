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
    Piano_Key2 = 4,
    Piano_Key3 = 8,
    No_Battery_Lantern = 16,
    Old_Battery = 32,
    Not_Rewinded_Audio_Tape = 64,
    No_Battery_Walkman = 128,
    Pen = 256,
    CabinetKey = 512,
    Map = 1024,
    Lantern = Old_Battery | No_Battery_Lantern,
    Walkman = Old_Battery | No_Battery_Walkman,
    Audio_Tape = Not_Rewinded_Audio_Tape | Pen,
}
