
/// <summary>
/// Class for every interactable item that the player can pick up to
/// add to an inventory. Extends InteractionItemBase
/// </summary>
public class InteractionItem : InteractionItemBase
{
    /// <summary>
    /// This method determines the action of the object when clicked
    /// </summary>
    public override void Execute()
    {
        Inventory inventory = FindObjectOfType<Inventory>();

        // Only Adds item if the inventory has a free slot
        byte count = 0;
        foreach (ScriptableItem item in inventory.Bag)
            if (item != null)
                count++;

        if (count < 8)
        {
            inventory.Bag.Add(info);
            Destroy(gameObject);
        }
    }
}
