using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMedicineCabinet : InteractionCommon
{

    private Inventory inventory;
    public bool IsOpen { get; private set; }
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        IsOpen = false;
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
                    Debug.Log("You have the key!");
                }

            }
        }
        if (!IsOpen) Debug.Log("You don't have the key!");

    }
    public override string ToString() => "Open Cabinet";

}




