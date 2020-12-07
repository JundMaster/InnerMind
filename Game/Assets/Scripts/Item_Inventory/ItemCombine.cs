public class ItemCombine : ICombinable<ScriptableItem>
{
    public void CombineItem(ScriptableItem item1, ScriptableItem item2,
                            Inventory inventory)
    {
        ListOfItems combinationResults =
            ListOfItems.Flashlight | ListOfItems.Walkman | ListOfItems.AudioTape;

        if (item1.CombinationItem != null && item2.CombinationItem != null)
        {
            if (combinationResults.HasFlag(item1.ID | item2.ID))
            {
                inventory.Bag.Remove(item1);
                inventory.Bag.Remove(item2);
                inventory.Bag.Add(item1.CombinationItem);
            }
        }
    }
};
