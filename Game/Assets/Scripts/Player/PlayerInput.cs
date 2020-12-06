using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public TypeOfControl CurrentControl { get; private set; }
    public float ZAxis { get; private set; }
    public float XAxis { get; private set; }
    public float HorizontalMouse { get; private set; }
    public float VerticalMouse { get; private set; }
    public bool LeftClick { get; private set; }
    private bool rightClick;
    public bool RightClick
    {
        get => rightClick;
        private set
        {
            rightClick = value;
            if (rightClick)
                OnChangeControlClick();
        }
    }
    public bool MiddleClick { get; private set; }
    public bool Pause { get; private set; }
    public bool Enter { get; private set; }
    public static float MouseSpeed { get; set; }

    // Mouse Cursor ICON position
    public Vector2Int CursorPosition { get; private set; }

    public void ChangeTypeOfControl(TypeOfControl control) =>
        CurrentControl = control;

    private void OnChangeControlClick()
    {
        ChangeControl?.Invoke();
    }
    public event Action ChangeControl;

    private void Start()
    {
        CursorPosition = new Vector2Int(30, 30);
    }

    private void Update()
    {
        MouseSpeed = PlayerPrefs.GetFloat("mouseSpeed");

        switch (CurrentControl)
        {
            case TypeOfControl.InGameplay:
                Cursor.lockState = CursorLockMode.Locked;

                // Gets movement axis
                ZAxis = Input.GetAxisRaw("Vertical");
                XAxis = Input.GetAxisRaw("Horizontal");

                // Gets mouse axis
                HorizontalMouse = Input.GetAxis("Mouse X");
                VerticalMouse = Input.GetAxis("Mouse Y");

                // Gets left click
                LeftClick = Input.GetButtonDown("Fire1");

                // Gets right click
                RightClick = Input.GetButtonDown("Fire2");
                // if (RightClick) OnChangeControlClick();

                // Gets middle click                
                MiddleClick = Input.GetButtonDown("Fire3");

                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.P);

                break;

            case TypeOfControl.InNPCInteraction:
                // Gets left click
                LeftClick = Input.GetButtonDown("Fire1");

                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.P);

                break;

            case TypeOfControl.InInventory:
                Cursor.lockState = CursorLockMode.Confined;

                // Gets right click
                RightClick = Input.GetButtonDown("Fire2");
                // if (RightClick) OnChangeControlClick();

                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.P);

                break;

            case TypeOfControl.InExamine:
                Cursor.lockState = CursorLockMode.Locked;
                // Gets left click
                LeftClick = Input.GetButton("Fire1");

                // Gets right click
                RightClick = Input.GetButtonDown("Fire2");
                // if (RightClick) OnChangeControlClick();

                // Gets the ESC key
                Pause = Input.GetKeyDown(KeyCode.P);

                // Gets horizontal movement
                HorizontalMouse = Input.GetAxis("Mouse X");

                // Get vertical movement
                VerticalMouse = Input.GetAxis("Mouse Y");

                break;

            case TypeOfControl.InDoorWithCode:
                Cursor.lockState = CursorLockMode.Confined;
                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.P);

                // Gets left click
                LeftClick = Input.GetButtonDown("Fire1");

                break;

            case TypeOfControl.InPauseMenu:
                Cursor.lockState = CursorLockMode.Confined;

                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.P);

                break;

            case TypeOfControl.InCutscene:
                Cursor.lockState = CursorLockMode.Locked;
                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.P);

                // Gets horizontal movement
                HorizontalMouse = Input.GetAxis("Mouse X");

                // Get vertical movement
                VerticalMouse = Input.GetAxis("Mouse Y");

                break;
        }
    }
}
