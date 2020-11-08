using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableItem : ScriptableObject, IItem, 
    ICombinable<ScriptableItem>, IComparable<ScriptableItem>
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


    public void CombineItem(ScriptableItem otherItem, Inventory inventory)
    {
        ListOfItems combinationResults = 
            ListOfItems.Lantern | ListOfItems.Walkman | ListOfItems.Audio_Tape;

        if (this.CombinationItem != null && otherItem.CombinationItem != null)
        {
            if (combinationResults.HasFlag(this.id | otherItem.ID))
            {
                inventory.Bag.Remove(this);
                inventory.Bag.Remove(otherItem);

                if (combinationItem != null)
                    inventory.Bag.Add(combinationItem);
            }
        }
    }

    int IComparable<ScriptableItem>.CompareTo(ScriptableItem other)
    {
        if (other == null) return 1;
        else return String.Compare(Name, other.Name);
    }
}
