using System.Collections;

/// <summary>
/// Interface for npc behaviours.
/// </summary>
public interface INPCMovementBehaviour
{
    /// <summary>
    /// Method that executes behaviour.
    /// </summary>
    void ExecuteBehaviour();

    /// <summary>
    /// Coroutine used to animate the npc.
    /// </summary>
    /// <returns>Returns animation.</returns>
    IEnumerator Behaviour();
}
