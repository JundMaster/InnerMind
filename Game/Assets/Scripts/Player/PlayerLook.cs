using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    
    public float VerticalRotation { get; set; }
    private float horizontalRotation;

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
            VerticalRotation -= input.VerticalMouse * 
                PlayerInput.MouseSpeed * Time.deltaTime;
            VerticalRotation = Mathf.Clamp(VerticalRotation, -70f, 70f);

            // Rotates the camera (vertical) around X axis
            transform.localRotation = Quaternion.Euler(VerticalRotation, 0f, 0f);

            // Rotates the player on Y axis (horizontal)
            player.Rotate(Vector3.up * input.HorizontalMouse * 
                PlayerInput.MouseSpeed * Time.deltaTime);
        }
        else if (PlayerInput.CurrentControl == TypeOfControl.InCutscene)
        {
            // Clamps vertical axis
            VerticalRotation -= input.VerticalMouse *
                PlayerInput.MouseSpeed * Time.deltaTime;
            VerticalRotation = Mathf.Clamp(VerticalRotation, -20f, 20f);

            // Clamps horizontal axis
            horizontalRotation += input.HorizontalMouse * 
                PlayerInput.MouseSpeed * Time.deltaTime;
            horizontalRotation = Mathf.Clamp(horizontalRotation, -20f, 20f);

            // Rotates with a limit position
            transform.localRotation = Quaternion.Euler(VerticalRotation, horizontalRotation, 0f);
        }
    }
}
