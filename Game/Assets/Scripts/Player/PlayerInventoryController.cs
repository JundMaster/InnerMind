using UnityEngine;
using UnityEngine.UI;


public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    // Components
    private PlayerInput input;

    // Controlling Items
    public ScriptableItem ClickedItemInfo { get; set; }

    private Vector2Int cursorPosition;

    // Actions
    private Interaction_Inventory_Combine combineItem;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        combineItem = new Interaction_Inventory_Combine();

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
        if (ClickedItemInfo == null)
        {
            ClickedItemInfo = item;
            Cursor.SetCursor(ClickedItemInfo.CursorTexture, cursorPosition, CursorMode.Auto);
        }
        else if (ClickedItemInfo != item)
        {
            combineItem.Execute(item);
        }
        else
        {
            ClickedItemInfo = null;
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

                    if (ClickedItemInfo != null)
                    {
                        ClickedItemInfo = null;
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
