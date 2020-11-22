using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionItem : InteractionItemBase
{
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

            // Sorts/reverses the list to eliminate free spaces at the beggining
            inventory.Bag.Sort();
            inventory.Bag.Reverse();

            Destroy(gameObject);
        }
    }
}
