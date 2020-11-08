﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item_InteractionBase : Interaction_Common
{
    [SerializeField] protected ScriptableItem info;

    public abstract override void Execute();

    public override string ToString()
    {
        return $"Pick up {info.Name}";
    }
}
