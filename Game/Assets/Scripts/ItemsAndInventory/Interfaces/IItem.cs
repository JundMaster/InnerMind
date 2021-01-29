using UnityEngine;

/// <summary>
/// Interface for every Item ingame
/// </summary>
public interface IItem
{
    /// <summary>
    /// Name of the item
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description of the item
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Item ID from ListOfItems
    /// </summary>
    ListOfItems ID { get; }

    /// <summary>
    /// Item's Icon, while the item is on inventory
    /// </summary>
    Sprite Icon { get; }

    /// <summary>
    /// Item's cursor texture, while the item is selected on inventory
    /// </summary>
    Texture2D CursorTexture { get; }

    /// <summary>
    /// Item's prefab
    /// </summary>
    GameObject Prefab { get; }

    /// <summary>
    /// If the item has a possible combination, this property has that
    /// combination item
    /// </summary>
    ScriptableItem CombinationItem { get; }

    /// <summary>
    /// Sound that plays when item is combined
    /// </summary>
    SoundClip CombinationSound { get; }
}
