using System;
using UnityEngine;

/// <summary>
/// Class for every item. This class is a ScriptableObject with 
/// [CreateAssetMenu]. Every item is created through this class on editor.
/// Implements IItem and IComparable
/// </summary>
[CreateAssetMenu]
public class ScriptableItem : ScriptableObject, IItem, 
    IComparable<ScriptableItem>
{
    [SerializeField] private new string name;
    [TextArea(1, 2)]
    [SerializeField] private string description;
    [SerializeField] private ListOfItems id;
    [SerializeField] private Sprite icon;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private GameObject prefab;
    [SerializeField] private ScriptableItem combinationItem;
    [SerializeField] private SoundClip combinationSound;

    /// <summary>
    /// Name of the item
    /// </summary>
    public string Name          { get => name; }

    /// <summary>
    /// Description of the item
    /// </summary>
    public string Description { get => description; }

    /// <summary>
    /// Item ID from ListOfItems
    /// </summary>
    public ListOfItems ID       { get => id; }

    /// <summary>
    /// Item's Icon, while the item is on inventory
    /// </summary>
    public Sprite Icon          { get => icon; }

    /// <summary>
    /// Item's cursor texture, while the item is selected on inventory
    /// </summary>
    public Texture2D CursorTexture { get => cursorTexture; }

    /// <summary>
    /// Item's prefab
    /// </summary>
    public GameObject Prefab    { get => prefab; }

    /// <summary>
    /// If the item has a possible combination, this property has that
    /// combination item
    /// </summary>
    public ScriptableItem CombinationItem { get => combinationItem; }

    /// <summary>
    /// Sound that plays when the item is combined
    /// </summary>
    public SoundClip CombinationSound { get => combinationSound; }

    /// <summary>
    /// Compares this ScriptableItem with another ScriptableItem
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(ScriptableItem other)
    {
        if (other == null) return 1;
        else return String.Compare(Name, other.Name);
    }
}
