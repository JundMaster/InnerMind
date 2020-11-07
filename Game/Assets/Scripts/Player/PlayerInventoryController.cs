using UnityEngine;


public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    // Components
    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        // Runs PickItem when an item is clicked
        inventory.OnItemClickEvent += UseItem;
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
