using UnityEngine;

/// <summary>
/// Class for assist mode. Uses sandbox pattern
/// </summary>
public abstract class AssistMode : MonoBehaviour
{
    // Enum with AssistModeType
    [SerializeField] protected AssistModeType assistModeType;

    // Enum with each puzzle to play Victory
    [SerializeField] protected PuzzlesEnum puzzleName;

    [SerializeField] protected ScriptableItem puzzlePrize;

    // Puzzle to play victory
    protected IPuzzle puzzleVariable;

    /// <summary>
    /// Abstract method to execute a certain action
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// Executes a puzzle victory condition
    /// </summary>
    /// <param name="puzzle">Puzzle to execute victory</param>
    protected void ExecuteVictory(IPuzzle puzzle)
    {
        puzzle.Victory();
    }

    /// <summary>
    /// Adds a scriptable item to inventory
    /// </summary>
    protected void AddPrizeToInventory()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        inventory.Bag.Add(puzzlePrize);
    }
}
