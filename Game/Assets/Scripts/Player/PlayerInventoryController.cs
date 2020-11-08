using UnityEngine;


public class PlayerInventoryController : MonoBehaviour
{
    private Inventory inventory;
    // Components
    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        // Runs UseItem when an item is clicked
        for (int i = 0; i < inventory.InventorySlot.Length; i++)
            inventory.InventorySlot[i].OnLeftClickEvent += UseItem;
    }

    private void Update()
    {
        ChangeControls();
    }

    private void UseItem(ScriptableItem item)
    {
        inventory.Bag.Remove(item);
    }

    private void ChangeControls()
    {
        if (input.RightClick)
        {
            switch (input.CurrentControl)
            {
                case TypeOfControl.InGameplay:
                    input.CurrentControl = TypeOfControl.InInventory;
                    break;
                case TypeOfControl.InInventory:
                    input.CurrentControl = TypeOfControl.InGameplay;
                    break;
            }
        }
    }
}
