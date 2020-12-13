
/// <summary>
/// Class that combines twi scriptable items and adds them (or not) to
/// an Inventory. Implements ICombinable
/// </summary>
public class ItemCombine : ICombinable<ScriptableItem>
{
    /// <summary>
    /// Compares two items and adds it to the inventory if the operation
    /// is valid
    /// </summary>
    /// <param name="item1">First item to compare</param>
    /// <param name="item2">Second item to compare</param>
    /// <param name="inventory">Inventory</param>
    public void CombineItem(ScriptableItem item1, ScriptableItem item2,
                            Inventory inventory)
    {
        // Valid item combination results
        ListOfItems combinationResults =
            ListOfItems.Flashlight | ListOfItems.WalkmanWithoutTape |
            ListOfItems.AudioTape | ListOfItems.Walkman;

        // Of both combination items are the same
        if (item1.CombinationItem == item2.CombinationItem)
        {
            // And a combination result has item 1 and item 2
            if (combinationResults.HasFlag(item1.ID | item2.ID))
            {
                // Removes both items from the bag and adds a new item
                inventory.Bag.Remove(item1);
                inventory.Bag.Remove(item2);
                inventory.Bag.Add(item1.CombinationItem);
            }
        }
    }
};
