using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRays : MonoBehaviour
{
    // Rays
    public Ray CheckGround { get; private set; }
    public Ray Forward { get; private set; }
    public Ray CameraForward { get; private set; }

    // Components
    private Camera mainCamera;
    private PlayerMovement movement;
   
    private void Start()
    {
        mainCamera = Camera.main;
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Ray to confirm if there isn't a wall/obstacle blocking the path
        CheckGround = new Ray(mainCamera.transform.position,
            movement.GroundCheck.transform.position - transform.position);

        // Creates a ray from playerCamera to transform.forward
        Forward = new Ray(mainCamera.transform.position, transform.forward);

        // Creates a ray from playerCamera to the mouse position (crosshair)
        CameraForward = mainCamera.ScreenPointToRay(Input.mousePosition);
    }
}
