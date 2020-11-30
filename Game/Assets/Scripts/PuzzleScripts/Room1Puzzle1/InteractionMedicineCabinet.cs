using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMedicineCabinet : InteractionCommon
{

    private Inventory inventory;
    public bool IsOpen { get; private set; }
    private Animator cabinetDoorAnimation;
    private BoxCollider closetBoxCollider;
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        IsOpen = false;
        cabinetDoorAnimation = GetComponentInChildren<Animator>();
        closetBoxCollider = GetComponent<BoxCollider>();
    }
    public override void Execute()
    {

        
        foreach (ScriptableItem item in inventory.Bag)
        {
            if (item != null)
            {
                if (item.ID == ListOfItems.CabinetKey)
                {
                    IsOpen = true;
                    closetBoxCollider.enabled = false;
                    cabinetDoorAnimation.SetTrigger("Open Door");
                    inventory.Bag.Remove(item);
                    break;
                }

            }
        }
        

    }
    public override string ToString() => "Open Cabinet";

}




