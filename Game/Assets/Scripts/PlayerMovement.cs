using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    [Range(1, 4)] [SerializeField] private float speed;
    private Vector3 movement;
    [SerializeField] private Transform groundCheck;

    // Wall walk room
    private bool inWallWalkRoom;
    private bool insideChangingFaceCR;

    // Components
    private Rigidbody rb;
    private PlayerInput input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        // Only runs if the player is in a WallWalkRoom
        if (inWallWalkRoom)
        {
            ChangeFace();
        }
    }

    private void Movement()
    {
        // Movement variable
        movement = (transform.right * input.XAxis + 
            transform.forward * input.ZAxis).normalized;

        // Refresh groundCheck position (position where player is moving towards)
        groundCheck.transform.position = transform.position + movement * 0.75f;

        // Used to check if there is something in front of the player
        Collider[] groundCheckCollider = 
            Physics.OverlapSphere(groundCheck.transform.position, 0.1f);

        // The player will only move if there is something in front)
        if (groundCheckCollider.Length == 0)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            // Moves player
            rb.velocity = movement * speed;
        }
    }

    private void ChangeFace()
    {
        // Creates a ray from camera to transform.forward
        Ray wallCheckRay = new Ray(Camera.main.transform.position, 
            transform.forward);

        // If collides with a wall and the player is moving forward
        if (Physics.Raycast(wallCheckRay, out RaycastHit hitInfo, 0.5f))
        {
            if (hitInfo.collider.gameObject.layer == 9)
            {
                if (input.ZAxis > 0)
                {
                    if (!insideChangingFaceCR)
                    {
                        StartCoroutine(CRChangeFace(hitInfo));
                        insideChangingFaceCR = true;
                    }
                }
            }
        }
        // Draws ray on editor
        Debug.DrawRay(Camera.main.transform.position, transform.forward * 0.5f,
            Color.red);
    }

    IEnumerator CRChangeFace(RaycastHit hitInfo)
    {
        // Stops time, rotates the player towards the point of impact
        Time.timeScale = 0f;
        rb.isKinematic = true;
        transform.LookAt(transform.position - hitInfo.normal, transform.up);

        // Changes position and rotation smoothly
        float elapsedTime = 0.0f;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(-90, 0f, 0f);
        while (elapsedTime < 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z), 
                elapsedTime / 0.5f);

            transform.rotation = Quaternion.Slerp(from, to, elapsedTime / 0.5f);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.rotation = to;
                                
        // Gives back all player control
        rb.isKinematic = false;
        insideChangingFaceCR = false;
        Time.timeScale = 1f;
    }


    // OnTriggers
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WallWalkRoom"))
        {
            inWallWalkRoom = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallWalkRoom"))
        {
            inWallWalkRoom = false;
        }
    }


    private void OnDrawGizmos()
    {
        // Axis Rays
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.up * 7f);
        Gizmos.DrawRay(transform.position, transform.right * 7f);

        // Wall Colide Ray
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, transform.forward * 0.5f);

        // GroundCheck Sphere
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.1f);
    }
}
