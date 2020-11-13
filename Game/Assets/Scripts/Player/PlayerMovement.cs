using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    [Range(1, 20)] [SerializeField] private byte speed;
    [SerializeField] private Transform groundCheck;
    public Transform GroundCheck { get => groundCheck; }
    [SerializeField] private LayerMask obstacleLayer;

    // Walkable walls room
    private Coroutine CR_ChangeWall;

    // Components
    private GameManager manager;
    private Rigidbody   rb;
    private PlayerInput input;
    private PlayerRays  rays;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        rays = GetComponent<PlayerRays>();
    }

    private void FixedUpdate()
    {
        Movement();

        // Only runs if the player is in a WallWalkRoom
        if (manager.CurrentTypeOfRoom == TypeOfRoom.WalkableWalls)
        {
            ChangeFace();
        }
    }

    private void Update()
    {
        
    }

    private void Movement()
    {
        Vector3 movement;
        // Movement variable
        movement = (transform.right * input.XAxis + 
            transform.forward * input.ZAxis).normalized;

        // Refresh groundCheck position (position where player is moving towards)
        groundCheck.transform.position = transform.position + movement * 0.5f;

        // Collider to check if there is a Walls_Floor layer in front
        Collider[] groundCheckCollider = 
            Physics.OverlapSphere(groundCheck.transform.position, 0.1f,
            obstacleLayer);

        // If in gameplay
        if (PlayerInput.CurrentControl == TypeOfControl.InGameplay)
        {
            // If there's no floor in front, the player will stop
            if (groundCheckCollider.Length == 0)
            {
                rb.velocity = Vector3.zero;
            }
            else
            {
                // If there's floor and a wall in the middle, the player will stop
                if (Physics.Raycast(rays.CheckGround, 0.6f, obstacleLayer))
                {
                    rb.velocity = Vector3.zero;
                }

                else
                {
                    // Else the player will move
                    rb.velocity = movement * speed;
                }
            }
        }
        else // If not in gameplay
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void ChangeFace()
    {
        // If collides with a wall
        if (Physics.Raycast(rays.Forward, out RaycastHit hit, 1f))
        {
            // only if the collider is a walkable layer
            if (hit.collider.gameObject.layer == 9)
            {
                // if the player is moving forward
                if (input.ZAxis > 0)
                {
                    // And the coroutine isn't already running
                    if (CR_ChangeWall == null)
                    {
                        CR_ChangeWall = StartCoroutine(CRChangeFace(hit));
                    }
                }
            }
        }
    }


    private IEnumerator CRChangeFace(RaycastHit hit)
    {
        // Stops time, rotates the player towards the point of impact
        Time.timeScale = 0f;
        rb.isKinematic = true;
        transform.LookAt(transform.position - hit.normal, transform.up);
        GetComponentInChildren<PlayerLook>().VerticalRotation = 0;

        // Changes position and rotation smoothly
        float elapsedTime = 0.0f;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(-90, 0f, 0f);
        while (elapsedTime < 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                hit.point, elapsedTime * 15f * Time.fixedUnscaledDeltaTime);

            transform.rotation = Quaternion.Slerp(from, to, elapsedTime / 0.5f);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.rotation = to;

        // Gives back all player control
        rb.isKinematic = false;
        CR_ChangeWall = default;
        Time.timeScale = 1f;
    }


    // OnTriggers
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WallWalkRoom"))
        {
            manager.CurrentTypeOfRoom = TypeOfRoom.WalkableWalls;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallWalkRoom"))
        {
            manager.CurrentTypeOfRoom = TypeOfRoom.NonWalkableWalls;
        }
    }

    private void OnDrawGizmos()
    {
        // Axis Rays
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.up * 7f);
        Gizmos.DrawRay(transform.position, transform.right * 7f);

        // GroundCheck Sphere
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.1f);
    }
}
