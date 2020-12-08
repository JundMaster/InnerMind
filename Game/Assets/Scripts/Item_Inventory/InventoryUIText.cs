using UnityEngine;
using TMPro;

/// <summary>
/// Class for UI text while on inventory
/// </summary>
public class InventoryUIText : MonoBehaviour
{
    // Panel with a black square
    [SerializeField] private GameObject panel;

    // Text for the item name
    [SerializeField] private TextMeshProUGUI objectName;

    // Components
    private Inventory inventory;

    /// <summary>
    /// Awake method for InventoryUIText
    /// </summary>
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();

        // Must be on awake because the event is created after this script
        // Registers event
        if (inventory.InventorySlot != null)
        {
            for (int i = 0; i < inventory.InventorySlot.Length; i++)
            {
                inventory.InventorySlot[i].SlotOver += ShowUI;
                inventory.InventorySlot[i].SlotLeave += HideUI;
            }
        }
    }

    /// <summary>
    /// OnDestroy method for InventoryUIText
    /// </summary>
    private void OnDestroy()
    {
        // Unregisters event
        if (inventory.InventorySlot != null)
        {
            for (int i = 0; i < inventory.InventorySlot.Length; i++)
            {
                inventory.InventorySlot[i].SlotOver -= ShowUI;
                inventory.InventorySlot[i].SlotLeave -= HideUI;
            }
        }
    }

    /// <summary>
    /// Shows black panel and updates text with item's name
    /// </summary>
    /// <param name="info"></param>
    private void ShowUI(ScriptableItem info)
    {
        panel.SetActive(true);
        objectName.text = info.Name;
    }

    /// <summary>
    /// Hides the panel and the text
    /// </summary>
    private void HideUI()
    {
        panel.SetActive(false);
    }
}
