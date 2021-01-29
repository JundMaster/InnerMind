using System;
using UnityEngine;

/// <summary>
/// Class to define every player input.
/// </summary>
public class PlayerInput : MonoBehaviour, IPlayerInput
{
    /// <summary>
    /// Property for player current control.
    /// </summary>
    public TypeOfControl CurrentControl { get; private set; }

    /// <summary>
    /// Property for ZAxis.
    /// </summary>
    public float ZAxis { get; private set; }

    /// <summary>
    /// Property for XAxis.
    /// </summary>
    public float XAxis { get; private set; }

    /// <summary>
    /// Propert for horizontal mouse speed.
    /// </summary>
    public float HorizontalMouse { get; private set; }

    /// <summary>
    /// Propert for vertical mouse speed.
    /// </summary>
    public float VerticalMouse { get; private set; }

    /// <summary>
    /// Propert for left click.
    /// </summary>
    public bool LeftClick { get; private set; }

    /// <summary>
    /// Propert for right click.
    /// </summary>
    public bool RightClick { get; private set; }

    private bool inventory;
    /// <summary>
    /// Property for right click.
    /// </summary>
    public bool Inventory
    {
        get => inventory;
        private set
        {
            inventory = value;
            if (inventory)
                OnChangeControlClick();
        }
    }

    /// <summary>
    /// Property for pause input.
    /// </summary>
    public bool Pause { get; private set; }

    /// <summary>
    /// Property to get a key to pass a scene.
    /// </summary>
    public bool Space { get; private set; }

    /// <summary>
    /// Property for cursor position relative to cursor in game.
    /// </summary>
    public Vector2Int CursorPosition { get; private set; }

    /// <summary>
    /// Property for mouse speed.
    /// </summary>
    public float MouseSpeed { get; set; }

    /// <summary>
    /// Property for mouse position.
    /// </summary>
    public Vector3 MousePosition { get; set; }

    /// <summary>
    /// Method to change type of control.
    /// </summary>
    /// <param name="control">TypeOfControl to change to</param>
    public void ChangeTypeOfControl(TypeOfControl control) =>
        CurrentControl = control;

    /// <summary>
    /// Method to invoke ChangeControl.
    /// </summary>
    private void OnChangeControlClick()
    {
        ChangeControl?.Invoke();
    }

    /// <summary>
    /// Event that happens when control is changed.
    /// </summary>
    public event Action ChangeControl;

    /// <summary>
    /// Start method for PlayerInput.
    /// </summary>
    private void Start()
    {
        CursorPosition = new Vector2Int(30, 30);
    }

    /// <summary>
    /// Update method for PlayerInput.
    /// </summary>
    private void Update()
    {
        MouseSpeed = PlayerPrefs.GetFloat("mouseSpeed");

        // Defines player controls depending on current TypeOfControl
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

                // Gets I key
                Inventory = Input.GetKeyDown(KeyCode.Tab);

                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.Escape);

                // Gets Space key
                Space = Input.GetKeyDown(KeyCode.Space);

                // Gets mouse position
                MousePosition = Input.mousePosition;

                break;

            case TypeOfControl.InNPCInteraction:
                // Gets left click
                LeftClick = Input.GetButtonDown("Fire1");

                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.Escape);

                break;

            case TypeOfControl.InInventory:
                Cursor.lockState = CursorLockMode.Confined;

                // Gets I key
                Inventory = Input.GetKeyDown(KeyCode.Tab);

                // Gets right click
                RightClick = Input.GetButtonDown("Fire2");

                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.Escape);

                break;

            case TypeOfControl.InExamine:
                Cursor.lockState = CursorLockMode.Locked;
                // Gets left click
                LeftClick = Input.GetButton("Fire1");

                // Gets right click
                RightClick = Input.GetButtonDown("Fire2");

                // Gets the ESC key
                Pause = Input.GetKeyDown(KeyCode.Escape);

                // Gets horizontal movement
                HorizontalMouse = Input.GetAxis("Mouse X");

                // Get vertical movement
                VerticalMouse = Input.GetAxis("Mouse Y");

                break;

            case TypeOfControl.InDoorWithCode:
                Cursor.lockState = CursorLockMode.Confined;
                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.Escape);

                // Gets left click
                LeftClick = Input.GetButtonDown("Fire1");

                // Gets right click
                RightClick = Input.GetButtonDown("Fire2");

                break;

            case TypeOfControl.InPauseMenu:
                Cursor.lockState = CursorLockMode.Confined;

                // Gets ESC key
                Pause = Input.GetKeyDown(KeyCode.Escape);

                break;

            case TypeOfControl.InCutscene:
                Cursor.lockState = CursorLockMode.Locked;

                // Gets horizontal movement
                HorizontalMouse = Input.GetAxis("Mouse X");

                // Get vertical movement
                VerticalMouse = Input.GetAxis("Mouse Y");

                // Gets Space key
                Space = Input.GetKeyDown(KeyCode.Space);

                break;

            case TypeOfControl.InTutorial:
                Cursor.lockState = CursorLockMode.Locked;

                // Gets Space key
                Space = Input.GetKeyDown(KeyCode.Space);

                break;
        }
    }
}
