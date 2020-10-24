using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Range(50, 250)][SerializeField] private ushort speed;
    private float verticalRotation;

    // Components
    private Transform player;
    private PlayerInput input;

    void Start()
    {
        input = GetComponentInParent<PlayerInput>();

        player = transform.parent.transform;
        verticalRotation = 0f;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Clamps vertical axis
        verticalRotation -= input.VerticalMouse * speed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

        // Rotates the camera (vertical) around X axis
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Rotates the player on Y axis (horizontal)
        player.Rotate(Vector3.up * input.HorizontalMouse * speed * Time.deltaTime);
    }
}
