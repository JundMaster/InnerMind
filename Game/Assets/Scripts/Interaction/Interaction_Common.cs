using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction_Common : MonoBehaviour, IInteractable
{
    public abstract void Execute();

    public abstract void Execute<T>(T other);
}
