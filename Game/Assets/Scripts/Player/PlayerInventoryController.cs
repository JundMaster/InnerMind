using UnityEngine;

/// <summary>
/// Class to define user control inside inventory.
/// </summary>
public class PlayerInventoryController : MonoBehaviour
{
    // Actions In inventory
    private ExaminerController examiner;
    private ExamineMenu examineMenu;
    private ItemCombine itemCombine;

    // Components
    private Inventory inventory;
    private PlayerInput input;
    private Animator anim;
    private PauseMenu pauseMenu;

    // Variable that keeps the item pressed in inventory
    private ScriptableItem LastClickedItemInfo;
    public ScriptableItem LastClickedActionsAvailable { get; private set; }

    /// <summary>
    /// Awake method for PlayerInventoryController.
    /// </summary>
    private void Awake()
    {
        examineMenu = FindObjectOfType<ExamineMenu>();
        examiner = FindObjectOfType<ExaminerController>();
        itemCombine = new ItemCombine();

        inventory = FindObjectOfType<Inventory>();
        input = GetComponent<PlayerInput>();
        anim = inventory.GetComponent<Animator>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    /// <summary>
    /// OnEnable method for PlayerInventoryController.
    /// </summary>
    private void OnEnable()
    {
        // Subscribes to events
        input.ChangeControl += ChangeControl;
        pauseMenu.PauseGameEvent += CancelLastClickItemInfo;
        if (inventory.InventorySlot != null)
        {
            for (int i = 0; i < inventory.InventorySlot.Length; i++)
            {
                inventory.InventorySlot[i].SlotClick += SelectItem;
                inventory.InventorySlot[i].ActionClick += ActionOnItem;
            }
        }
    }

    /// <summary>
    /// OnDisableMethod for PlayerInventoryController.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribes events
        input.ChangeControl -= ChangeControl;
        pauseMenu.PauseGameEvent -= CancelLastClickItemInfo;
        for (int i = 0; i < inventory.InventorySlot.Length; i++)
        {
            inventory.InventorySlot[i].SlotClick -= SelectItem;
            inventory.InventorySlot[i].ActionClick -= ActionOnItem;
        }
    }

    /// <summary>
    /// Sets LastClickedItemInfo variable to null.
    /// </summary>
    private void CancelLastClickItemInfo()
        => LastClickedItemInfo = null;

    /// <summary>
    /// Runs actions to select an item in inventory.
    /// This method is responsible for changing the cursor and trying to
    /// combine two items.
    /// </summary>
    /// <param name="item">Information about the ScriptableItem pressed.
    /// This is the item the player pressed on inventory.</param>
    private void SelectItem(ScriptableItem item)
    {
        // If the icon was selected with right mouse button, it's set to null
        LastClickedActionsAvailable = null;

        // Selects the item and changes the cursor to that item's cursor 
        // texture
        if (LastClickedItemInfo == null)
        {
            LastClickedItemInfo = item;
            Cursor.SetCursor(LastClickedItemInfo.CursorTexture,
                            input.CursorPosition, CursorMode.Auto);
        }
        // If the new selected item is different from the other item
        else if (LastClickedItemInfo != item)
        {
            // Tries to combine the item
            itemCombine.CombineItem(item, LastClickedItemInfo, inventory);
            // Sets last item clicked to null and resets cursor
            LastClickedItemInfo = null;
            Cursor.SetCursor(default, input.CursorPosition, CursorMode.Auto);
        }
    }

    /// <summary>
    /// If the user pressed right mouse button, a variable is 'filled' with
    /// the last clicked item
    /// </summary>
    private void ActionOnItem(ScriptableItem item)
    {
        // If the icon was selected with left mouse button, it's set to null
        LastClickedItemInfo = null;

        LastClickedActionsAvailable = item;

        Cursor.SetCursor(LastClickedActionsAvailable.CursorTexture,
                            input.CursorPosition, CursorMode.Auto);
    }

    /// <summary>
    /// After the user pressed inventory from PlayerInput, this method
    /// does something depending on the type of control. Shows the inventory, 
    /// hides the inventory or sets the cursor and lastClickedItem to default.
    /// It also triggers inventory animations.
    /// </summary>
    private void ChangeControl()
    {
        //
        switch (input.CurrentControl)
        {
            case TypeOfControl.InGameplay:
            {
                // Shows inventory
                anim.SetTrigger("showInventory");
                anim.ResetTrigger("hideInventory");
                break;
            }

            case TypeOfControl.InInventory:
            {
                // If there's an item selected
                // Removes the item from the mouse
                if (LastClickedActionsAvailable != null)
                {
                    LastClickedActionsAvailable = null;

                    Cursor.SetCursor(default,
                                    input.CursorPosition,
                                    CursorMode.Auto);
                }
                else
                {
                    anim.SetTrigger("hideInventory");
                    anim.ResetTrigger("showInventory");
                }
                break;
            }

            case TypeOfControl.InExamine:
            {
                // If there's a selected item in the mouse
                if (LastClickedActionsAvailable == null)
                {   
                    // And the player is in examine
                    if (examiner != null)
                    {
                        // Destroys the examiner and goes back to inventory
                        examiner.DestroyExaminer();

                        input.ChangeTypeOfControl(
                                    TypeOfControl.InInventory);
                        examineMenu.HideDisplayMenu();
                    }
                }
                break;
            }
        }
    }

    /// <summary>
    /// Uses an item if the item is IUsable. Called on editor, on use
    /// button on inventory.
    /// </summary>
    public void Use()
    {
        if (LastClickedActionsAvailable != null)
        {
            if (LastClickedActionsAvailable.Prefab.
                TryGetComponent(out IUsable item))
            {
                item.Use();
            }
        }
    }

    /// <summary>
    /// Examines an item. Called on editor, on examine
    /// button on inventory.
    /// </summary>
    public void Examine()
    {
        if (LastClickedActionsAvailable != null)
        {
            Camera examineCamera = FindObjectOfType<ExamineMenu>().ExamineCamera;
            input.ChangeTypeOfControl(TypeOfControl.InExamine);
            examiner.SetExaminer(new ItemExaminer(5, LastClickedActionsAvailable,
                                examineCamera));
            examineMenu.DisplayExamineMenu();
        }
    }

    /// <summary>
    /// Update method for PlayerInventoryController.
    /// </summary>
    private void Update()
    {
        // Right click cancels examine
        if (input.CurrentControl == TypeOfControl.InExamine)
        {
            if (input.RightClick)
            {
                examiner.DestroyExaminer();

                input.ChangeTypeOfControl(
                            TypeOfControl.InInventory);
                examineMenu.HideDisplayMenu();
            }
        }

        // Right click cancels selected item
        if (LastClickedItemInfo != null ||
            LastClickedActionsAvailable != null)
        {
            if (input.RightClick)
            {
                LastClickedActionsAvailable = null;
                LastClickedItemInfo = null;

                Cursor.SetCursor(default,
                                input.CursorPosition,
                                CursorMode.Auto);
            }
        }
    }
}
