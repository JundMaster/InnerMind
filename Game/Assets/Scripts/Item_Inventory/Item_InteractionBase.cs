using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item_InteractionBase : Interaction_Common
{
    [SerializeField] protected ScriptableItem info;

    public override void InteractionAction()
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

    public override string ToString()
    {
        return $"Pick up {info.Name}";
    }
}
