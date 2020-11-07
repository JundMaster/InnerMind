using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // List of items that player has ( what player actually has on inventory )
    public List<ScriptableItem> Bag { get; set; }

    // List of items UI ( what player sees on the bar )
    [SerializeField] private Item_InventorySlot[] itemInInventorySlot;

    // Evet to get clicks on Inventory Slots
    public event Action<ScriptableItem> OnItemClickEvent;

    private void Awake()
    {
        for (int i = 0; i < itemInInventorySlot.Length; i++)
        {
            itemInInventorySlot[i].OnLeftClickEvent += OnItemClickEvent;
        }


        // Creates a list with 8 slots
        Bag = new List<ScriptableItem>(new ScriptableItem[8]);
    }

    private void Update()
    {
        // Sorts and reverses the list to eliminate free spaces at the beggining
        Bag.Sort();
        Bag.Reverse();

        // Sets the UI items equal to bag items
        for (int i = 0; i < itemInInventorySlot.Length; i++)
        {
            itemInInventorySlot[i].Info = Bag[i];
        }
    }

}
