using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventorySlot : MonoBehaviour, IPointerClickHandler, 
    IPointerEnterHandler, IPointerExitHandler
{
    // This variable will be equal to a item in the player's bag
    public ScriptableItem Info { get; set; }

    private Image image;

    // Click event
    public event Action<ScriptableItem, PointerEventData.InputButton> SlotClick;

    // Mouse over event
    public event Action<ScriptableItem> SlotOver;
    public event Action<ScriptableItem> SlotLeave;

    public PointerEventData.InputButton inputButton { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData != null)
        {
            if (Info != null && SlotClick != null)
            {
                inputButton = eventData.button;
                SlotClick?.Invoke(Info, inputButton);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (Info != null)
            {
                SlotOver?.Invoke(Info);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData != null)
        {
            if (Info != null)
            {
                SlotLeave?.Invoke(Info);
            }
        }
    }

    // If the ScritableItem icon changes, the image changes in game
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
