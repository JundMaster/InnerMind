using UnityEngine;

/// <summary>
/// Abstract Class for every interactable item that the player can pick up to
/// add to an inventory. Extends InteractionCommon
/// </summary>
public abstract class InteractionItemBase : InteractionCommon
{
    // Every item has a ScriptableItem
    [SerializeField] protected ScriptableItem info;

    /// <summary>
    /// This method determines the action of the object when clicked
    /// </summary>
    public abstract override void Execute();

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of the item
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        return $"Pick up {info.Name}";
    }
}
