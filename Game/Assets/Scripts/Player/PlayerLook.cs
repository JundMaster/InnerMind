using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Range(50, 250)][SerializeField] private ushort speed;
    public float VerticalRotation { get; set; }

    // Components
    private Transform player;
    private PlayerInput input;

    private void Start()
    {
        input = GetComponentInParent<PlayerInput>();

        player = transform.parent.transform;
        VerticalRotation = 0f;
    }

    private void Update()
    {
        if (PlayerInput.CurrentControl == TypeOfControl.InGameplay)
        {
            // Clamps vertical axis
            VerticalRotation -= input.VerticalMouse * speed * Time.deltaTime;
            VerticalRotation = Mathf.Clamp(VerticalRotation, -70f, 70f);

            // Rotates the camera (vertical) around X axis
            transform.localRotation = Quaternion.Euler(VerticalRotation, 0f, 0f);

            // Rotates the player on Y axis (horizontal)
            player.Rotate(Vector3.up * input.HorizontalMouse * speed * Time.deltaTime);
        }
    }
}
