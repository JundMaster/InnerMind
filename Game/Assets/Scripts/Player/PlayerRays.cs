using UnityEngine;

/// <summary>
/// Class responsible for creating rays to be used in player's raycasts
/// </summary>
public class PlayerRays : MonoBehaviour
{
    /// <summary>
    /// Property for ray to ground
    /// </summary>
    public Ray CheckGround { get; private set; }

    /// <summary>
    /// Property for ray to forward
    /// </summary>
    public Ray Forward { get; private set; }

    /// <summary>
    /// Property for ray to mouse position
    /// </summary>
    public Ray RayToMouse { get; private set; }

    // Components
    private Camera mainCamera;
    private PlayerMovement movement;
   
    /// <summary>
    /// Start method of PlayerRays
    /// </summary>
    private void Start()
    {
        mainCamera = Camera.main;
        movement = GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// Update method of PlayerRays
    /// </summary>
    private void Update()
    {
        // Ray to confirm if there isn't a wall/obstacle blocking the path
        CheckGround = new Ray(mainCamera.transform.position,
            movement.GroundCheck.transform.position - transform.position);

        // Creates a ray from playerCamera to transform.forward
        Forward = new Ray(mainCamera.transform.position, transform.forward);

        // Creates a ray from playerCamera to the mouse position (crosshair)
        RayToMouse = mainCamera.ScreenPointToRay(Input.mousePosition);
    }
}
