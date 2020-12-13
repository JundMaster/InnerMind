using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Class for Obtained Items that go to inventory
/// </summary>
public class InventoryUIObtainItem : MonoBehaviour
{
    // Canvas to control
    [SerializeField] private GameObject canvas;

    // Image to print
    [SerializeField] private Image image;

    private Inventory inventory;
    
    /// <summary>
    /// Awake method for InventoryUIObtainItem
    /// </summary>
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    /// <summary>
    /// OnEnable method for InventoryUIObtainItem
    /// </summary>
    private void OnEnable()
    {
        inventory.Bag.AddedItem += ShowImage;
    }

    /// <summary>
    /// OnDisable method for InventoryUIObtainItem
    /// </summary>
    private void OnDisable()
    {
        inventory.Bag.AddedItem -= ShowImage;
    }

    /// <summary>
    /// Method that calls a coroutine
    /// </summary>
    /// <param name="item"></param>
    private void ShowImage(ScriptableItem item) => 
        StartCoroutine(ShowImageCoroutine(item));

    /// <summary>
    /// Coroutine that shows an image of an obtained item
    /// </summary>
    /// <param name="item">Item to display the image</param>
    /// <returns>Returns a new waitforseconds</returns>
    private IEnumerator ShowImageCoroutine(ScriptableItem item)
    {
        image.sprite = item.Icon;
        canvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        canvas.SetActive(false);
    }
}
