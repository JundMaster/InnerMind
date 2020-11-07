using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_InventorySlot : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField] private ScriptableItem info;
    public ScriptableItem Info { get; set; }

    private Image image;


    // Event to get left click
    public event Action<ScriptableItem> OnLeftClickEvent;

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
    }

    // Refreshes slot image on editor
    private void OnValidate()
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

    // If the ScritableItem icon changes, the image changes
    private void Update()
    {
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
