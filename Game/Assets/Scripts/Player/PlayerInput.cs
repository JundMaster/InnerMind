using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public TypeOfControl CurrentControl { get; set; }
    public float ZAxis { get; private set; }
    public float XAxis { get; private set; }
    public float HorizontalMouse { get; private set; }
    public float VerticalMouse { get; private set; }
    public bool LeftClick { get; private set; }
    public bool RightClick { get; private set; }

    private void Update()
    {
        if (CurrentControl == TypeOfControl.InGameplay)
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Gets movement axis
            ZAxis = Input.GetAxisRaw("Vertical");
            XAxis = Input.GetAxisRaw("Horizontal");

            // Gets mouse axis
            HorizontalMouse = Input.GetAxis("Mouse X");
            VerticalMouse = Input.GetAxis("Mouse Y");

            // Gets left click
            LeftClick = Input.GetButtonDown("Fire1");
        }
        else if (CurrentControl == TypeOfControl.InNPCInteraction)
        {
            // Gets left click
            LeftClick = Input.GetButtonDown("Fire1");
        }
        else if (CurrentControl == TypeOfControl.InInventory)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
