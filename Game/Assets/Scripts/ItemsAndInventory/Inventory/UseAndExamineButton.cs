using UnityEngine;

/// <summary>
/// Class with the responsibility of finding UseAndExamineButton, so it
/// can be used in inventory Use and Examine buttons
/// </summary>
public class UseAndExamineButton : MonoBehaviour
{
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject examineButton;

    // Components
    private PlayerInventoryController inventoryController;
    private PlayerInput input;

    /// <summary>
    /// Awake method for UseAndExamineButton.
    /// </summary>
    private void Awake()
    {
        inventoryController = FindObjectOfType<PlayerInventoryController>();
        input = FindObjectOfType<PlayerInput>();
    }

    /// <summary>
    /// Updated method for UseAndExamineButton.
    /// Refreshes UI depending on the item selected.
    /// If the item is usable it shows 'use' icon, otherwise it only shows
    /// 'examine icon.
    /// </summary>
    private void Update()
    {
        if (input?.CurrentControl == TypeOfControl.InInventory)
        {
            if (inventoryController.LastClickedActionsAvailable != null)
            {
                // If the item is usable, shows the 'use' icon
                if (inventoryController.LastClickedActionsAvailable.Prefab.
                    TryGetComponent(out IUsable temp))
                {
                    if (useButton?.activeSelf == false)
                        useButton?.SetActive(true);
                }
                else
                {
                    if (useButton?.activeSelf == true)
                        useButton?.SetActive(false);
                }

                // shows 'examine' icon
                if (examineButton?.activeSelf == false)
                    examineButton?.SetActive(true);
            }
            else
            {
                // If no item is selected, doesn't show the icons
                if (useButton?.activeSelf == true)
                    useButton?.SetActive(false);
                if (examineButton?.activeSelf == true)
                    examineButton?.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Calls PlayerInventoryController Use().
    /// </summary>
    public void Use() => inventoryController.Use();

    /// <summary>
    /// Calls PlayerInventoryController Examine().
    /// </summary>
    public void Examine() => inventoryController.Examine();
}
