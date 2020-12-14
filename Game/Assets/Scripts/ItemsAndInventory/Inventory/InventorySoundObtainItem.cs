using UnityEngine;

/// <summary>
/// Class for Sound when obtaining a new item
/// </summary>
public class InventorySoundObtainItem : MonoBehaviour
{
    private Inventory inventory;

    /// <summary>
    /// Awake method for InventoryUIObtainItem
    /// </summary>
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    /// <summary>
    /// OnEnable method for InventorySoundObtainItem
    /// </summary>
    private void OnEnable()
    {
        inventory.Bag.AddedItem += PlaySound;
    }

    /// <summary>
    /// OnDisable method for InventorySoundObtainItem
    /// </summary>
    private void OnDisable()
    {
        inventory.Bag.AddedItem -= PlaySound;
    }

    /// <summary>
    /// Plays pickup item sound
    /// </summary>
    /// <param name="item">ScriptableItem to match with delegate</param>
    private void PlaySound(ScriptableItem item)
    {
        SoundManager.PlaySound(SoundClip.PickUpObject);
    }
}
