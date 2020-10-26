﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    [Range(1, 20)] [SerializeField] private byte speed;
    private Vector3 movement;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask walkableLayer;

    // Wall walk room
    private TypeOfRoom currentRoomType;
    private bool cr_runningChangeFace;

    // Components
    private Rigidbody rb;
    private PlayerInput input;
    private PlayerLook camera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        camera = GetComponentInChildren<PlayerLook>();

        currentRoomType = TypeOfRoom.NonWalkableWalls;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        // Only runs if the player is in a WallWalkRoom
        if (currentRoomType == TypeOfRoom.WalkableWalls)
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
        groundCheck.transform.position = transform.position + movement * 0.5f;

        // Collider to check if there is a Walls_Floor layer in front
        Collider[] groundCheckCollider = 
            Physics.OverlapSphere(groundCheck.transform.position, 0.1f, 
            walkableLayer);

        // Ray to confirm if there isn't a wall/obstacle blocking the path
        Ray checkObstacle = new Ray(Camera.main.transform.position, 
            groundCheck.transform.position - transform.position);

        // If there's no floor in front, the player will stop
        if (groundCheckCollider.Length == 0)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            // If there's floor and a wall in the middle, the player will stop
            if (Physics.Raycast(checkObstacle, 0.6f, walkableLayer))
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

    private void ChangeFace()
    {
        // Creates a ray from camera to transform.forward
        Ray wallCheckRay = new Ray(Camera.main.transform.position, 
            transform.forward);

        // If collides with a wall
        if (Physics.Raycast(wallCheckRay, out RaycastHit hit, 1f))
        {
            // only if the collider is a walkable layer
            if (hit.collider.gameObject.layer == 9)
            {
                // if the player is moving forward
                if (input.ZAxis > 0)
                {
                    // And the coroutine isn't already running
                    if (!cr_runningChangeFace)
                    {
                        StartCoroutine(CRChangeFace(hit));
                        cr_runningChangeFace = true;
                    }
                }
            }
        }
    }

    IEnumerator CRChangeFace(RaycastHit hit)
    {
        // Stops time, rotates the player towards the point of impact
        Time.timeScale = 0f;
        rb.isKinematic = true;
        transform.LookAt(transform.position - hit.normal, transform.up);
        camera.VerticalRotation = 0;

        // Changes position and rotation smoothly
        float elapsedTime = 0.0f;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(-90, 0f, 0f);
        while (elapsedTime < 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                hit.point, elapsedTime / 20f);

            transform.rotation = Quaternion.Slerp(from, to, elapsedTime / 0.5f);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.rotation = to;
                                
        // Gives back all player control
        rb.isKinematic = false;
        cr_runningChangeFace = false;
        Time.timeScale = 1f;
    }


    // OnTriggers
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WallWalkRoom"))
        {
            currentRoomType = TypeOfRoom.WalkableWalls;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallWalkRoom"))
        {
            currentRoomType = TypeOfRoom.NonWalkableWalls;
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
        Gizmos.DrawRay(Camera.main.transform.position, transform.forward * 1f);

        // GroundCheck Sphere
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.1f);
    }
}
