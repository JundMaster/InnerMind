using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract class for npc behaviours. Implements INPCMovementBehaviour.
/// </summary>
public abstract class NPCMovementBehaviour : MonoBehaviour, INPCMovementBehaviour
{
    /// <summary>
    /// Method that executes a behaviour.
    /// </summary>
    public void ExecuteBehaviour()
    {
        StartCoroutine(Behaviour());
    }

    /// <summary>
    /// Abstract npc behaviour.
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator Behaviour();
}
