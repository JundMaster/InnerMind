using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // List of items that player has ( what player actually has on inventory )
    public List<ScriptableItem> Bag { get; set; }

    // List of items UI ( what player sees on the bar )
    [SerializeField] private Item_InventorySlot[] itemInInventoryUI;

    private void Awake()
    {
        // Creates a list with 8 slots
        Bag = new List<ScriptableItem>(new ScriptableItem[8]);
    }

    private void Update()
    {
        // Sorts and reverses the list to eliminate free spaces at the beggining
        Bag.Sort();
        Bag.Reverse();

        // Sets the UI items equal to bag items
        for (int i = 0; i < itemInInventoryUI.Length; i++)
        {
            itemInInventoryUI[i].Info = Bag[i];
        }
    }

}
