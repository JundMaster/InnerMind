using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionCommon : MonoBehaviour, IInteract
{
    public abstract void Execute();
}
