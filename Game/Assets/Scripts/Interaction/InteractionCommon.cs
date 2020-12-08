using UnityEngine;

/// <summary>
/// Abstract class for interactions. Implements IInteract.
/// </summary>
public abstract class InteractionCommon : MonoBehaviour, IInteract
{
    /// <summary>
    /// This method determines the action of the object when clicked
    /// </summary>
    public abstract void Execute();
}
