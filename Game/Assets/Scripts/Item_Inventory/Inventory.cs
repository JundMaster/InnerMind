using System.Collections.Specialized;
using UnityEngine;
using System.IO;

public class Inventory : MonoBehaviour
{
    // List of items that player has ( what player actually has on inventory )
    public ObservableList<ScriptableItem> Bag;

    // List of items in player's inventory
    public ItemInventorySlot[] InventorySlot { get; private set; }

    [SerializeField] private ScriptableItem[] possibleItems;

    // Inventory File
    private FileReader fileReader;

    private void Awake()
    {
        // Creates a list with 8 slots
        Bag = new ObservableList<ScriptableItem>(new ScriptableItem[8]);

        InventorySlot = new ItemInventorySlot[8];

        // Fills every InventorySlot index with Item_InventorySlot in children
        InventorySlot = GetComponentInChildren<Transform>().
                        GetComponentsInChildren<ItemInventorySlot>();

        // Reads txt with inventory info ( if it already exists )
        if (File.Exists(FilePath.inventoryPath))
        {
            fileReader = new FileReader(FilePath.inventoryPath);
            fileReader.ReadFromTXT(Bag, possibleItems);
            UpdateUI();
        }
    }
    private void OnEnable()
    {
        Bag.CollectionChanged += UpdateUI;
        Bag.CollectionChanged += WriteTXT;
    }

    private void OnDisable()
    {
        Bag.CollectionChanged -= UpdateUI;
        Bag.CollectionChanged -= WriteTXT;
    }

    private void UpdateUI(object sender = null, 
        NotifyCollectionChangedEventArgs e = null)
    {
        SortBag();
        for (int i = 0; i < InventorySlot.Length; i++)
        {
            InventorySlot[i].Info = Bag[i];
        }
    }

    private void WriteTXT(object sender = null, 
        NotifyCollectionChangedEventArgs e = null)
    {
        FileWriter fw = new FileWriter(FilePath.inventoryPath);
        fw.AddToTxt(Bag);
    }

    private void SortBag()
    {
        Bag.Sort();
        Bag.Reverse();
    }

    private void OnApplicationQuit()
    {
        File.Delete(FilePath.inventoryPath);
    }
}

