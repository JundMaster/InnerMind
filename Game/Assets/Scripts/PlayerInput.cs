using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float ZAxis { get; private set; }
    public float XAxis { get; private set; }
    public float HorizontalMouse { get; private set; }
    public float VerticalMouse { get; private set; }

    void Update()
    {
        // Gets movement axis
        ZAxis = Input.GetAxisRaw("Vertical");
        XAxis = Input.GetAxisRaw("Horizontal");

        // Gets mouse axis
        HorizontalMouse = Input.GetAxis("Mouse X");
        VerticalMouse = Input.GetAxis("Mouse Y");
    }
}
