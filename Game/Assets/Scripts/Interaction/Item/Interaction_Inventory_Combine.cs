using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Inventory_Combine : Interaction_Common
{
    private ScriptableItem CombineItem(ScriptableItem other)
    {
        ScriptableItem newCombinedItem = new ScriptableItem();

        //  Flags stuff
        Debug.Log("Combine item");

        if (true)
            return newCombinedItem;
        else
            return null;
    }

    public override void Execute<T>(T parameter)
    {
        ScriptableItem other = parameter as ScriptableItem;

        CombineItem(other);
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}
