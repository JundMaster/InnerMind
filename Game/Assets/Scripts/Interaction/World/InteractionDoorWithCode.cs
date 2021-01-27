using UnityEngine;
using System;

/// <summary>
/// Class for every interactable door with a code
/// Extends InteractionCommon
/// </summary>
public class InteractionDoorWithCode : InteractionCommon
{
    // Code required to open padlock
    [SerializeField] private CustomVector3 doorCode;

    [SerializeField] private PuzzlesEnum myPuzzle;

    public CustomVector3 DoorCode => doorCode;

    // The user code
    public CustomVector3 UserCode { get; set; }

    // Padlock to spawn
    [SerializeField] private GameObject padlock;
    private GameObject newPadlock;

    // Padlock spawn related variables
    [SerializeField] private GameObject padlockCanvas;
    [SerializeField] private Transform padlockPosition;

    // Components
    private IPlayerInput input;
    private PlayerGeneralInfo playerInfo;

    /// <summary>
    /// Awake method for InteractionDoorWithCode
    /// </summary>
    private void Awake() => playerInfo = FindObjectOfType<PlayerGeneralInfo>();

    /// <summary>
    /// Start method for InteractionDoorWithCode
    /// </summary>
    private void Start() => input = FindObjectOfType<PlayerInput>();

    /// <summary>
    /// This method determines the action of the door when clicked
    /// </summary>
    public override void Execute()
    {
        // If the code hasn't been inserted yet
        if (UserCode != doorCode &&
            playerInfo.PuzzlesDone.HasFlag(myPuzzle) == false)
        {
            //Instantiates a new padlock that the user can interact with////////
            input.ChangeTypeOfControl(TypeOfControl.InDoorWithCode);
            padlockCanvas.SetActive(true);

            UserCode = new CustomVector3(0, 0, 0);
            newPadlock = Instantiate(padlock);
            newPadlock.transform.position = padlockPosition.position;
            newPadlock.transform.rotation = padlockPosition.rotation;
            ////////////////////////////////////////////////////////////////////
        }
    }

    /// <summary>
    /// Opens the locked door
    /// </summary>
    public void OpenDoor()
    {
        Animator doorAnimation = GetComponentInParent<Animator>();
        doorAnimation.SetTrigger("Open Door");

        gameObject.layer = 2;

        // Calls the event DoorOpened
        OnDoorOpened();

        // Goes back to gameplay
        BackToGameplay();
    }

    /// <summary>
    /// Method that invokes DoorOpened event
    /// </summary>
    protected virtual void OnDoorOpened()
    {
        DoorOpened?.Invoke();
    }

    /// <summary>
    /// Event for DoorOpened
    /// </summary>
    public event Action DoorOpened;

    /// <summary>
    /// Goes back to gameplay
    /// This method is also called from PadlockButton when Back button is
    /// pressed, while in the padlock action
    /// </summary>
    public void BackToGameplay()
    {
        Destroy(newPadlock);
        padlockCanvas.SetActive(false);
        input.ChangeTypeOfControl(TypeOfControl.InGameplay);
    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of the door
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "Open Locked Door";
}
