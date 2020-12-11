using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class to define user control inside inventory
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

    /// <summary>
    /// Awake method for PlayerInventoryController
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
    /// OnEnable method for PlayerInventoryController
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
            }
        }
    }

    /// <summary>
    /// OnDisableMethod for PlayerInventoryController
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribes events
        input.ChangeControl -= ChangeControl;
        pauseMenu.PauseGameEvent -= CancelLastClickItemInfo;
        for (int i = 0; i < inventory.InventorySlot.Length; i++)
        {
            inventory.InventorySlot[i].SlotClick -= SelectItem;
        }
    }

    /// <summary>
    /// Sets LastClickedItemInfo variable to null
    /// </summary>
    private void CancelLastClickItemInfo()
        => LastClickedItemInfo = null;

    /// <summary>
    /// Runs actions to select an item in inventory
    /// This method is responsible for changing the cursor and trying to
    /// combine two items
    /// </summary>
    /// <param name="item">Information about the ScriptableItem pressed.
    /// This is the item the player pressed on inventory</param>
    private void SelectItem(ScriptableItem item)
    {
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
    /// After the user pressed RightClick from PlayerInput, this method
    /// does something depending on type of control. Shows the inventory, 
    /// hides the inventory or sets the cursor and lastClickedItem to default
    /// It also triggers inventory animations
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
                if (LastClickedItemInfo != null)
                {
                    LastClickedItemInfo = null;
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
                if (LastClickedItemInfo == null)
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
    /// button on inventory
    /// </summary>
    public void Use()
    {
        if (LastClickedItemInfo != null)
        {
            if (LastClickedItemInfo.Prefab.TryGetComponent(out IUsable item))
            {
                item.Use();
            }
        }
    }

    /// <summary>
    /// Examines an item. Called on editor, on examine
    /// button on inventory
    /// </summary>
    public void Examine()
    {
        if (LastClickedItemInfo != null)
        {
            Camera examineCamera = FindObjectOfType<ExamineMenu>().ExamineCamera;
            input.ChangeTypeOfControl(TypeOfControl.InExamine);
            examiner.SetExaminer(new ItemExaminer(5, LastClickedItemInfo,
                                examineCamera));
            examineMenu.DisplayExamineMenu();
        }
    }

    /// <summary>
    /// Update method for PlayerInventoryController
    /// </summary>
    private void Update()
    {
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
    }
}
