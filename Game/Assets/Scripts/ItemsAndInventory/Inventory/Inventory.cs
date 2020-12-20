using System.Collections.Specialized;
using UnityEngine;
using System.IO;

/// <summary>
/// Class for an inventory
/// </summary>
public class Inventory : MonoBehaviour
{
    // List of items that the player has (what player actually has on inventory)
    public ObservableList<ScriptableItem> Bag;

    // List of slots that construct the inventory
    public ItemInventorySlot[] InventorySlot { get; private set; }

    // Variable to compare items in inventory
    private ItemComparer comparer;

    // FileReader that will be used to read player's items from a txt
    // This file has a list with all items in inventory
    private FileReader fileReader;

    /// <summary>
    /// Awake method for Inventory
    /// </summary>
    private void Awake()
    {
        // Creates a list with 8 slots
        Bag = new ObservableList<ScriptableItem>(new ScriptableItem[8]);

        // Creates an array with 8 slots
        InventorySlot = new ItemInventorySlot[8];

        // Fills every InventorySlot index with Item_InventorySlot in children
        InventorySlot = GetComponentInChildren<Transform>().
                        GetComponentsInChildren<ItemInventorySlot>();

        comparer = FindObjectOfType<ItemComparer>();

        // Reads txt with inventory info (if it already exists)
        // If it exists, it means the inventory will have this items
        // when a scene is loaded
        if (File.Exists(FilePath.inventoryPath))
        {
            // Reads file from inventorypath
            fileReader = new FileReader(FilePath.inventoryPath);
            // Updates de bag and ui with items from the .txt
            fileReader.ReadFromTXT(Bag, comparer.PossibleItems);
            UpdateUI();
        }
    }

    /// <summary>
    /// OnEnable method from Inventory
    /// </summary>
    private void OnEnable()
    {
        Bag.CollectionChanged += UpdateUI;
        Bag.CollectionChanged += WriteTXT;
    }

    /// <summary>
    /// OnDisable method from Inventory
    /// </summary>
    private void OnDisable()
    {
        Bag.CollectionChanged -= UpdateUI;
        Bag.CollectionChanged -= WriteTXT;
    }

    /// <summary>
    /// This method updates UI depending on the inventory's bag
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">Information about the event</param>
    private void UpdateUI(object sender = null, 
        NotifyCollectionChangedEventArgs e = null)
    {
        SortBag();
        // for every slots in inventory, matches the inventory slot UI
        // with the items in inventory's bag
        for (int i = 0; i < InventorySlot.Length; i++)
        {
            InventorySlot[i].Info = Bag[i];
        }
    }

    /// <summary>
    /// This method writes what's on the inventory's bag to a .txt
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">Information about the event</param>
    private void WriteTXT(object sender = null, 
        NotifyCollectionChangedEventArgs e = null)
    {
        FileWriter fw = new FileWriter(FilePath.inventoryPath);
        fw.AddToTxt(Bag);
    }

    /// <summary>
    /// This method sorts the bag, so the items are ordered
    /// </summary>
    private void SortBag()
    {
        Bag.Sort();
        Bag.Reverse();
    }
}

