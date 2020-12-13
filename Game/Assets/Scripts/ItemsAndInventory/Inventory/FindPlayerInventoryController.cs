using UnityEngine;

/// <summary>
/// Class with the responsibility of finding PlayerInventoryController, so it
/// can be used in inventory Use and Examine buttons
/// </summary>
public class FindPlayerInventoryController : MonoBehaviour
{
    private PlayerInventoryController inventoryController;

    /// <summary>
    /// Awake method for FindPlayerInventoryController
    /// </summary>
    private void Awake()
    {
        inventoryController = FindObjectOfType<PlayerInventoryController>();
    }

    /// <summary>
    /// Calls PlayerInventoryController Use()
    /// </summary>
    public void Use() => inventoryController.Use();

    /// <summary>
    /// Calls PlayerInventoryController Examine()
    /// </summary>
    public void Examine() => inventoryController.Examine();
}
