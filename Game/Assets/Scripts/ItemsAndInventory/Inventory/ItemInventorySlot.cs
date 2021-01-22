using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class for Slots that construct an inventory.
/// Implements IPointerClickHandler for click events.
/// Implements IPointerEnterHandler for mouse over events.
/// Implements IPointerExitHandler for mouse over events.
/// </summary>
public class ItemInventorySlot : MonoBehaviour, IPointerClickHandler, 
    IPointerEnterHandler, IPointerExitHandler
{
    // This variable will be equal to a item in the player's bag
    public ScriptableItem Info { get; set; }

    // Image for the inventory slot
    private Image image;

    // Click event
    public event Action<ScriptableItem> SlotClick;      // left click
    public event Action<ScriptableItem> ActionClick;    // right click

    // Mouse over event
    public event Action<ScriptableItem> SlotOver;
    public event Action SlotLeave;

    /// <summary>
    /// Method that happens when the user clicks the mouse in this slot.
    /// </summary>
    /// <param name="eventData">Information about the click</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && 
            eventData.button == PointerEventData.InputButton.Left)
        {
            if (Info != null)
            {
                // Contains information about which item is in this slot
                SlotClick?.Invoke(Info);
            }
        }

        if (eventData != null &&
            eventData.button == PointerEventData.InputButton.Right)
        {
            if (Info != null)
            {
                // Contains information about which item is in this slot
                ActionClick?.Invoke(Info);
            }
        }
    }

    /// <summary>
    /// Method that happens when the user passes the mouse over this slot
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (Info != null)
            {
                // Contains information about which item is in this slot
                SlotOver?.Invoke(Info);
            }
        }
    }

    /// <summary>
    /// Method that happens when the user takes off the mouse of this slot.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (Info != null)
            {
                SlotLeave?.Invoke();
            }
        }
    }

    /// <summary>
    /// Update method for ItemInventorySlot
    /// If the ScritableItem icon changes, the image changes in game.
    /// </summary>
    private void Update()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (Info == null)
        {
            image.enabled = false;
        }
        else
        {
            image.sprite = Info.Icon;
            image.enabled = true;
        }
    }
}
