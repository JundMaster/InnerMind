using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    private Examiner examiner;

    // Components
    private PlayerInput input;
    private Animator anim;

    // Controlling Items
    private ScriptableItem LastClickedItemInfo;


    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        anim = inventory.GetComponent<Animator>();
    }

    private void Start()
    {
        // Runs UseItem when an item is clicked
        for (int i = 0; i < inventory.InventorySlot.Length; i++)
        {
            inventory.InventorySlot[i].OnLeftClickEvent += SelectItem;
            inventory.InventorySlot[i].OnMiddleClickEvent += ExamineItem;
        }
    }

    private void Update()
    {
        ChangeControls();

        if (PauseMenu.Gamepaused) LastClickedItemInfo = null;
    }

    // The item is the item that the player clicked
    private void ExamineItem(ScriptableItem item)
    {
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InExamine);
        examiner = FindObjectOfType<Examiner>();
        examiner.SetExaminer(new ItemExaminer(5, item, ExamineMenu.ExamineCamera));
    }

    // The item is the item that the player clicked
    private void SelectItem(ScriptableItem item)
    {
        // Selects the item and changes the cursor
        if (LastClickedItemInfo == null)
        {
            LastClickedItemInfo = item;
            Cursor.SetCursor(LastClickedItemInfo.CursorTexture,
                            PlayerInput.CursorPosition, CursorMode.Auto);
        }
        // If the new selected item is different from the other item
        else if (LastClickedItemInfo != item)
        {
            LastClickedItemInfo.CombineItem(item, inventory);
            LastClickedItemInfo = null;
            Cursor.SetCursor(default, PlayerInput.CursorPosition, CursorMode.Auto);
        }
    }


    private void ChangeControls()
    {
        if (PlayerInput.CurrentControl == TypeOfControl.InGameplay ||
            PlayerInput.CurrentControl == TypeOfControl.InInventory ||
             PlayerInput.CurrentControl == TypeOfControl.InExamine)
        {
            if (input.RightClick || input.MiddleClick)
            {
                switch (PlayerInput.CurrentControl)
                {
                    case TypeOfControl.InGameplay:

                        anim.SetTrigger("showInventory");
                        break;

                    case TypeOfControl.InInventory:

                        if (LastClickedItemInfo != null)
                        {
                            LastClickedItemInfo = null;
                            Cursor.SetCursor(default, PlayerInput.CursorPosition,
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
                                PlayerInput.ChangeTypeOfControl(TypeOfControl.InInventory);
                            }
                        }
                        break;

                }
            }
        }
    }
}
