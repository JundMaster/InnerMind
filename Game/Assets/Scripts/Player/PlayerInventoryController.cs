using UnityEngine;
using UnityEngine.UI;


public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    // Components
    private PlayerInput input;

    // Controlling Items
    public ScriptableItem LastClickedItemInfo { get; set; }

    private Vector2Int cursorPosition;


    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        cursorPosition = new Vector2Int (30, 30);
    }

    private void Start()
    {
        // Runs UseItem when an item is clicked
        for (int i = 0; i < inventory.InventorySlot.Length; i++)
            inventory.InventorySlot[i].OnLeftClickEvent += SelectItem;
    }

    private void Update()
    {
        ChangeControls();
    }

    // The item is the item that the player clicked
    private void SelectItem(ScriptableItem item)
    {
        // Selects the item and changes the cursor
        if (LastClickedItemInfo == null)
        {
            LastClickedItemInfo = item;
            Cursor.SetCursor(LastClickedItemInfo.CursorTexture, 
                            cursorPosition, CursorMode.Auto);
        }
        // If the new selected item is different from the other item
        else if (LastClickedItemInfo != item)
        {
            LastClickedItemInfo.CombineItem(item, inventory);
            LastClickedItemInfo = null;
            Cursor.SetCursor(default, cursorPosition, CursorMode.Auto);
        }
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

                    if (LastClickedItemInfo != null)
                    {
                        LastClickedItemInfo = null;
                        Cursor.SetCursor(default, cursorPosition, CursorMode.Auto);
                    }
                    else
                    {
                        input.CurrentControl = TypeOfControl.InGameplay;
                    }
                    break;
            }
        }
    }
}
