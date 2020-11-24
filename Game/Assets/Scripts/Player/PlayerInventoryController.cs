using UnityEngine;
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
        // Runs UseItem when an item is clicked
        for (int i = 0; i < inventory.InventorySlot.Length; i++)
        {
            inventory.InventorySlot[i].OnLeftClickEvent += SelectItem;
            inventory.InventorySlot[i].OnMiddleClickEvent += ExamineItem;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < inventory.InventorySlot.Length; i++)
        {
            inventory.InventorySlot[i].OnLeftClickEvent -= SelectItem;
            inventory.InventorySlot[i].OnMiddleClickEvent -= ExamineItem;
        }
    }

    private void Update()
    {
        ChangeControl();

        if (pauseMenu.Gamepaused) LastClickedItemInfo = null;
    }

    // The item is the item that the player clicked
    private void ExamineItem(ScriptableItem item)
    {
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
        if (input.CurrentControl == TypeOfControl.InGameplay  ||
            input.CurrentControl == TypeOfControl.InInventory ||
            input.CurrentControl == TypeOfControl.InExamine)
        {
            if (input.RightClick || input.MiddleClick)
            {
                switch (input.CurrentControl)
                {
                    case TypeOfControl.InGameplay:

                        anim.SetTrigger("showInventory");
                        break;

                    case TypeOfControl.InInventory:

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

                    case TypeOfControl.InExamine:
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
}
