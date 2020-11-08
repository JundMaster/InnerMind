using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    string Name { get; }
    ListOfItems ID { get; }
    Sprite Icon { get; }
    Texture2D CursorTexture { get; }
    GameObject Prefab { get; }
}
