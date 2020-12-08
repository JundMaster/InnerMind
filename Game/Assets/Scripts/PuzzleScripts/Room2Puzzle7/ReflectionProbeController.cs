using UnityEngine;

/// <summary>
/// Class responsible for controlling a reflect probe movement and position
/// </summary>
public class ReflectionProbeController : MonoBehaviour
{
    // Components
    private PlayerMovement playerMovement;

    // Desired position on inspector
    [SerializeField] private float positionZ = 35f;

    /// <summary>
    /// Start method of ReflectionProbeController
    /// </summary>
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    /// <summary>
    /// Update method of ReflectionProbeController
    /// </summary>
    private void Update()
    {
        // Controls the position of the probe depending on the position
        // of the player
        transform.position = new Vector3(playerMovement.transform.position.x,
            (positionZ - playerMovement.transform.position.z) / 6f, 
            positionZ - playerMovement.transform.position.z / 2);
    }
}
