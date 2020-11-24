using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventorySlot : MonoBehaviour, IPointerClickHandler
{
    // This variable will be equal to a item in the player's bag
    public ScriptableItem Info { get; set; }

    private Image image;

    // Click event
    public event Action<ScriptableItem, PointerEventData.InputButton> SlotClick;

    public PointerEventData.InputButton inputButton { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData != null)
        {
            if (Info != null && SlotClick != null)
            {
                inputButton = eventData.button;
                SlotClick.Invoke(Info, inputButton);
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
