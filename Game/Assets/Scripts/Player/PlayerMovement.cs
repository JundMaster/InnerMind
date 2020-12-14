using System.Collections;
using UnityEngine;

/// <summary>
/// Class responsible for player movement
/// </summary>
public class PlayerMovement : MonoBehaviour, ICoroutineT<RaycastHit>
{
    // Movement variables
    [Range(1, 20)] [SerializeField] private byte speed;
    public Vector3 Movement { get; private set; }

    // Groundcheck variables
    [SerializeField] private Transform groundCheck;
    /// <summary>
    /// Poperty that returns groundCheck
    /// </summary>
    public Transform GroundCheck { get => groundCheck; }

    // Layer to check if there's an obstacle between the player and the floor
    [SerializeField] private LayerMask obstacleLayer;

    // Walkable walls coroutine variable to control the coroutine
    public Coroutine ThisCoroutine { get; private set; }

    // Components
    private PlayerGeneralInfo playerInfo;
    private Rigidbody   rb;
    private PlayerInput input;
    private PlayerRays  rays;

    /// <summary>
    /// Start method of PlayerMovement
    /// </summary>
    private void Start()
    {
        playerInfo = GetComponent<PlayerGeneralInfo>();
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        rays = GetComponent<PlayerRays>();
    }

    /// <summary>
    /// FixedUpdate of PlayerMovement
    /// </summary>
    private void FixedUpdate()
    {
        Walk();
    }

    /// <summary>
    /// Update of PlayerMovement
    /// </summary>
    private void Update()
    {
        // Only runs if the player is in a WallWalkRoom
        if (playerInfo.CurrentTypeOfRoom == TypeOfRoom.WalkableWalls)
        {
            ChangeFace();
        }
    }

    /// <summary>
    /// This method is responsible for controlling the player's Movement
    /// </summary>
    private void Walk()
    {
        // Movement variable
        Movement = (transform.right * input.XAxis +
            transform.forward * input.ZAxis).normalized;

        // Refresh groundCheck position (position where player is moving towards)
        groundCheck.transform.position = transform.position + Movement * 0.5f;

        // Collider to check if there is a Walls_Floor layer in front
        Collider[] groundCheckCollider = 
            Physics.OverlapSphere(groundCheck.transform.position, 0.1f,
            obstacleLayer);

        // If in gameplay
        if (input.CurrentControl == TypeOfControl.InGameplay)
        {
            // If there's no floor in front, the player will stop
            if (groundCheckCollider.Length == 0)
            {;
                rb.velocity = Vector3.zero;
            }
            else
            {
                // Else the player will move
                rb.velocity = Movement * speed;
            }
        }
        else // If not in gameplay
        {
            rb.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Method responsible to check if the player is going to change walls
    /// </summary>
    private void ChangeFace()
    {
        // If collides with a wall
        if (Physics.Raycast(rays.Forward, out RaycastHit hit, 1f))
        {
            // only if the collider is a walkable layer
            if (hit.collider.gameObject.layer == 9)
            {
                // if the player is moving forward
                if (input.ZAxis > 0 && input.Space)
                {
                    // And the coroutine isn't already running
                    if (ThisCoroutine == null)
                    {
                        ThisCoroutine = StartCoroutine(CoroutineExecute(hit));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Coroutine responsible for executing and animating wall change action
    /// </summary>
    /// <param name="hit">Receives the wall point the player hit</param>
    /// <returns></returns>
    public IEnumerator CoroutineExecute(RaycastHit hit)
    {
        rb.isKinematic = true;

        // Rotates camera to wall and resets its value
        transform.LookAt(transform.position - hit.normal, transform.up);
        GetComponentInChildren<PlayerLook>().VerticalRotation = 0;

        // Sets values for desired rotation
        float elapsedTime = 0.0f;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(-90, 0f, 0f);

        // Translates position and rotation smoothly
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;

            // Translates smoothly
            transform.position = Vector3.MoveTowards(transform.position, 
                hit.point, elapsedTime / 2f);

            // Rotates smoothly
            transform.rotation = Quaternion.Slerp(from, to, elapsedTime * 2f);
            
            yield return null;
        }
        // Sets final position to hit.point
        transform.position = hit.point;
        transform.rotation = to;

        // Gives back all player control
        rb.isKinematic = false;
        ThisCoroutine = default;
    }

    /// <summary>
    /// OnTriggerStay of PlayerMovement
    /// </summary>
    /// <param name="other">Collider the player collided with</param>
    private void OnTriggerStay(Collider other)
    {
        // While inside a wallwalkroom, current type of control is set to
        // TypeOfRoom.WalkableWalls
        if (other.CompareTag("WallWalkRoom"))
        {
            playerInfo.CurrentTypeOfRoom = TypeOfRoom.WalkableWalls;
        }
    }

    /// <summary>
    /// OnTriggerExit of PlayerMovement
    /// </summary>
    /// <param name="other">Collider the player collided with</param>
    private void OnTriggerExit(Collider other)
    {
        // When leaving a wallwalkroom, current type of control is set to
        // TypeOfRoom.NonWalkableWalls
        if (other.CompareTag("WallWalkRoom"))
        {
            playerInfo.CurrentTypeOfRoom = TypeOfRoom.NonWalkableWalls;
        }
    }

    /*
    /// <summary>
    /// OnDrawGizmos of PlayerMovement
    /// Draws rays and a sphere only on editor
    /// </summary>
    private void OnDrawGizmos()
    {
        // Axis Rays
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.up * 7f);
        Gizmos.DrawRay(transform.position, transform.right * 7f);

        // GroundCheck Sphere
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.1f);
    }*/
}
