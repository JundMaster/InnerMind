using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableItem : ScriptableObject, IItem, 
    IComparable<ScriptableItem>
{
    [SerializeField] private new string name;
    [SerializeField] private ListOfItems id;
    [SerializeField] private Sprite icon;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private GameObject prefab;
    [SerializeField] private ScriptableItem combinationItem;

    public string Name          { get => name; }
    public ListOfItems ID       { get => id; }
    public Sprite Icon          { get => icon; }
    public Texture2D CursorTexture { get => cursorTexture; }
    public GameObject Prefab    { get => prefab; }
    public ScriptableItem CombinationItem { get => combinationItem; }

    int IComparable<ScriptableItem>.CompareTo(ScriptableItem other)
    {
        if (other == null) return 1;
        else return String.Compare(Name, other.Name);
    }
}
