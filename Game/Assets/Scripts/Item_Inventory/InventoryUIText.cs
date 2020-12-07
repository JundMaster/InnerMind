using UnityEngine;
using TMPro;

public class InventoryUIText : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI objectName;

    // Components
    private Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();

        // Must be on awake because the event is created after this script
        if (inventory.InventorySlot != null)
        {
            for (int i = 0; i < inventory.InventorySlot.Length; i++)
            {
                inventory.InventorySlot[i].SlotOver += ShowUI;
                inventory.InventorySlot[i].SlotLeave += HideUI;
            }
        }
    }

    private void OnDestroy()
    {
        if (inventory.InventorySlot != null)
        {
            for (int i = 0; i < inventory.InventorySlot.Length; i++)
            {
                inventory.InventorySlot[i].SlotOver -= ShowUI;
                inventory.InventorySlot[i].SlotLeave -= HideUI;
            }
        }
    }

    private void ShowUI(ScriptableItem info)
    {
        panel.SetActive(true);
        objectName.text = info.Name;
    }
    private void HideUI(ScriptableItem info)
    {
        panel.SetActive(false);
    }
}
