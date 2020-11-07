using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_InventorySlot : MonoBehaviour
{
    //[SerializeField] private ScriptableItem info;
    public ScriptableItem Info { get; set; }

    private Image image;

    #region// Refreshes image on editor
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
    #endregion

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
