using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventoryController : MonoBehaviour
{
    private Inventory inventory;

    // Actions In inventory
    private Examiner examiner;
    private ItemCombine itemCombine;

    // Components
    private PlayerInput input;
    private Animator anim;

    private PauseMenu pauseMenu;

    // Controlling Items
    private ScriptableItem LastClickedItemInfo;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();

        examiner = FindObjectOfType<Examiner>();
        itemCombine = new ItemCombine();

        input = GetComponent<PlayerInput>();
        anim = inventory.GetComponent<Animator>();

        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnEnable()
    {
        input.ChangeControl += ChangeControl;
        // Debug.Log("PlayerInventoryController");
        // Runs UseItem when an item is clicked
        for (int i = 0; i < inventory.InventorySlot.Length; i++)
        {
            inventory.InventorySlot[i].SlotClick += ClickAction;
        }
    }

    private void OnDisable()
    {
        input.ChangeControl -= ChangeControl;
        for (int i = 0; i < inventory.InventorySlot.Length; i++)
        {
            inventory.InventorySlot[i].SlotClick -= ClickAction;
        }
    }

    private void Update()
    {
        if (pauseMenu.Gamepaused) LastClickedItemInfo = null;
    }

    private void ClickAction(ScriptableItem item,
                             PointerEventData.InputButton inputButton)
    {
        if (inputButton == PointerEventData.InputButton.Left)
            SelectItem(item);

        else if (inputButton == PointerEventData.InputButton.Middle)
            ExamineItem(item);

    }

    // The item is the item that the player clicked
    private void ExamineItem(ScriptableItem item)
    {
        if (LastClickedItemInfo != null)
            return;
            
        Camera examineCamera = FindObjectOfType<ExamineMenu>().ExamineCamera;
        input.ChangeTypeOfControl(TypeOfControl.InExamine);
        examiner.SetExaminer(new ItemExaminer(5, item, examineCamera));
    }

    // The item is the item that the player clicked
    private void SelectItem(ScriptableItem item)
    {
        // Selects the item and changes the cursor
        if (LastClickedItemInfo == null)
        {
            LastClickedItemInfo = item;
            Cursor.SetCursor(LastClickedItemInfo.CursorTexture,
                            input.CursorPosition, CursorMode.Auto);
        }
        // If the new selected item is different from the other item
        else if (LastClickedItemInfo != item)
        {
            itemCombine.CombineItem(item, LastClickedItemInfo, inventory);
            LastClickedItemInfo = null;
            Cursor.SetCursor(default, input.CursorPosition, CursorMode.Auto);
        }
    }

    // Changes actions depending on the current control type
    private void ChangeControl()
    {
        switch (input.CurrentControl)
        {
            case TypeOfControl.InGameplay:
                {
                    anim.SetTrigger("showInventory");
                    break;
                }

            case TypeOfControl.InInventory:
                {
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
                    }
                    break;
                }

            case TypeOfControl.InExamine:
                {
                    if (LastClickedItemInfo == null)
                    {
                        if (examiner != null)
                        {
                            examiner.DestroyExaminer();

                            input.ChangeTypeOfControl(
                                        TypeOfControl.InInventory);
                        }
                    }
                    break;
                }

        }
    }
}
