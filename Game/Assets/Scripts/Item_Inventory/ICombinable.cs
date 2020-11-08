using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombinable<T> where T : ScriptableItem
{
    void CombineItem(T otherItem, Inventory inventory);
}
