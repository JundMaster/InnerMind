using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    [Range(1, 4)] [SerializeField] private float speed;
    private Vector3 movement;
    float zAxis, xAxis; 

    // Components
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Movement variable
        movement = transform.right * xAxis + transform.forward * zAxis;
        // Moves player
        rb.velocity = movement.normalized * speed;
    }


    void Update()
    {
        // Gets movement axis
        zAxis = Input.GetAxisRaw("Vertical");
        xAxis = Input.GetAxisRaw("Horizontal");
    }
}
