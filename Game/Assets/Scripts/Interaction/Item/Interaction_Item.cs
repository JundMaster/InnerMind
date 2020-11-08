using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Item : Item_InteractionBase
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
            Destroy(gameObject);
        }
    }
}
