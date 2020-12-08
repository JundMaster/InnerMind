using UnityEngine;

/// <summary>
/// Class for player interaction with IInteract objects
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    // Distance of interaction, set in editor
    [Range(0, 10)] [SerializeField] private byte interactDistance;

    /// <summary>
    /// Property to get interactDistance
    /// </summary>
    public byte InteractDistance { get => interactDistance; }

    // Components
    private PlayerInput input;
    private PlayerRays ray;

    /// <summary>
    /// Start method for PlayerInteract
    /// </summary>
    private void Start()
    {
        input = GetComponent<PlayerInput>();
        ray = GetComponent<PlayerRays>();
    }

    /// <summary>
    /// Update method for PlayerInteract
    /// </summary>
    private void Update()
    {
        // If in gameplay
        if (input.CurrentControl == TypeOfControl.InGameplay)
        {
            // If leftclick
            if (input.LeftClick)
            {
                // If there's an object inside interact distance
                // it returns that object as RaycastHit hit
                if (Physics.Raycast(ray.RayToMouse, out RaycastHit hit,
                    interactDistance))
                {
                    // Uses that information to check if the object is
                    // an IInteract object
                    if (hit.collider.gameObject.TryGetComponent
                        (out IInteract other))
                    {
                        // if it's an IInteract, it plays Execute() method
                        other.Execute();
                    }
                }
            }
        }
    }
}
