using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // List of items that player has ( what player actually has on inventory )
    public ObservableList<ScriptableItem> Bag;

    // List of items in player's inventory
    public ItemInventorySlot[] InventorySlot { get; private set; }

    private void Awake()
    {
        // Creates a list with 8 slots
        Bag = new ObservableList<ScriptableItem>(new ScriptableItem[8]);
        
        InventorySlot = new ItemInventorySlot[8];

        // GetComponentInChildren<Transform>() is the "Grid" child
        // Fills every InventorySlot index with Item_InventorySlot in children
        InventorySlot = GetComponentInChildren<Transform>().
                        GetComponentsInChildren<ItemInventorySlot>();
    }
    private void OnEnable()
    {
        Bag.CollectionChanged += UpdateUI;
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable Inventory");
        Bag.CollectionChanged -= UpdateUI;
    }

    private void UpdateUI(object sender, NotifyCollectionChangedEventArgs e)
    {
        Debug.Log("UpdateUI has been INVOKED!!!");
        SortBag();
        for (int i = 0; i < InventorySlot.Length; i++)
        {
            InventorySlot[i].Info = Bag[i];
        }
    }

    private void SortBag()
    {
        Bag.Sort();
        Bag.Reverse();
    }
}
