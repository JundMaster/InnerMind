using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Range(50, 250)][SerializeField] private float speed;
    private Transform player;
    private float verticalRotation;

    void Start()
    {
        player = this.transform.parent.GetComponent<Transform>();
        verticalRotation = 0f;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal    = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        float vertical      = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;

        // Clamps vertical axis
        verticalRotation -= vertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);
        // Rotates (vertical) around X axis
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Rotates the player on Y axis
        player.Rotate(Vector3.up * horizontal);
    }
}
