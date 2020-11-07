using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableItem : ScriptableObject, IItem, IComparable<ScriptableItem>
{
    [SerializeField] private new string name;
    [SerializeField] private ListOfItems id;
    [SerializeField] private Sprite icon;
 

    public string Name      { get => name; }
    public ListOfItems ID   { get => id; }
    public Sprite Icon      { get => icon; }

    int IComparable<ScriptableItem>.CompareTo(ScriptableItem other)
    {
        if (other == null) return 1;
        else return String.Compare(Name, other.Name);
    }
}
