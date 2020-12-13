
/// <summary>
/// Class responsible for controlling room1 puzzle. Extends PuzzleBase
/// </summary>
public class Room1 : PuzzleBase
{
    //Components
    private InteractionMedicineCabinet cabinet;

    /// <summary>
    /// Property that checks if the puzzle was finished
    /// </summary>
    public bool FinishedPuzzle { get; private set; }

    /// <summary>
    /// Awake method of Room1. This awake is overriding puzzlebase Awake
    /// </summary>
    private void Awake()
    {
        player = FindObjectOfType<PlayerGeneralInfo>();
        inventory = FindObjectOfType<Inventory>();

        cabinet = FindObjectOfType<InteractionMedicineCabinet>();
        FinishedPuzzle = false;
    }

    private void OnEnable()
    {
        cabinet.CabinetDoorOpened += Victory;
    }

    private void OnDisable()
    {
        cabinet.CabinetDoorOpened -= Victory;
    }

    /// <summary>
    /// Does an action when the puzzle is solved
    /// </summary>
    public override void Victory()
    {
        base.Victory();
        FinishedPuzzle = true;
    }
}
