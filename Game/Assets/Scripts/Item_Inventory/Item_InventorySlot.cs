using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_InventorySlot : MonoBehaviour, IPointerClickHandler
{
    // This variable will be equal to a item in the player's bag
    public ScriptableItem Info { get; set; }

    private Image image;

    // Events on clicks
    public event Action<ScriptableItem> OnLeftClickEvent;
    public event Action<ScriptableItem> OnMiddleClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Gets left click
        if (eventData != null && eventData.button ==
            PointerEventData.InputButton.Left)
        {
            if (Info != null && OnLeftClickEvent != null)
            {
                // Gets left click as a ScriptableItem
                OnLeftClickEvent(Info);
            }
        }

        if (eventData != null && eventData.button ==
            PointerEventData.InputButton.Middle)
        {
            if(Info != null && OnMiddleClickEvent != null)
            {
                OnMiddleClickEvent(Info);
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
