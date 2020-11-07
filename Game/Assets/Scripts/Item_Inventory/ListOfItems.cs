using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum ListOfItems
{
    Family_Photograph = 1,
    Piano_Key = 2,
    No_Battery_Lantern = 4,
    Old_Battery = 8,
    Audio_Tape = 16,
    Walkman = 32,
    Pen = 64,
    Lantern = Old_Battery | No_Battery_Lantern
}
