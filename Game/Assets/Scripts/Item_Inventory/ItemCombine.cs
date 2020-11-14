using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCombine : ICombinable<ScriptableItem>
{
    public void CombineItem(ScriptableItem item1, ScriptableItem item2, 
                            Inventory inventory)
    {
        ListOfItems combinationResults =
            ListOfItems.Lantern | ListOfItems.Walkman | ListOfItems.Audio_Tape;

        if (item1.CombinationItem != null && item2.CombinationItem != null)
        {
            if (combinationResults.HasFlag(item1.ID | item2.ID))
            {
                inventory.Bag.Remove(item1);
                inventory.Bag.Remove(item2);

                inventory.Bag.Add(item1.CombinationItem);

                inventory.Bag.Sort();
                inventory.Bag.Reverse();
            }
        }
    }
};
