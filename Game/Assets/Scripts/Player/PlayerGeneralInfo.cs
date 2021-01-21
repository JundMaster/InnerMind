using UnityEngine;

/// <summary>
/// Class used to keep player general information, like puzzles done
/// </summary>
public class PlayerGeneralInfo : MonoBehaviour
{
    // Editor variables to control startingControl and startingRoom
    // when the player spawns somewhere
    [SerializeField] private TypeOfControl startingControl;
    [SerializeField] private TypeOfRoom startingRoom;

    // Components
    private IPlayerInput input;

    /// <summary>
    /// Property to know which puzzles the player has already done
    /// </summary>
    public PuzzlesEnum PuzzlesDone { get; set; }

    // Current puzle the player is in (for assist mode only)
    private PuzzlesEnum currentPuzzle;

    /// <summary>
    /// Property with which type of room the player is currently in
    /// </summary>
    public TypeOfRoom CurrentTypeOfRoom { get; set; }

    /// <summary>
    /// Awake method for PlayerGeneralInfo
    /// </summary>
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// Start method for PlayerGeneralInfo
    /// </summary>
    private void Start()
    {
        input.ChangeTypeOfControl(startingControl);
        CurrentTypeOfRoom = startingRoom;
        PuzzlesDone = PuzzlesEnum.Default;
    }

}
