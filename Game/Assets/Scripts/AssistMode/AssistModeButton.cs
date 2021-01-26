using UnityEngine;

/// <summary>
/// Class for each button from AssistMode menu.
/// Plays victory condition for each puzzle ingame
/// </summary>
public class AssistModeButton : AssistMode
{
    /// <summary>
    /// Awake method for AssistModeButton
    /// </summary>
    private void Awake()
    {
        switch(puzzleName)
        {
            // Odd Morning
            case PuzzlesEnum.Puzzle1:
                puzzleVariable = FindObjectOfType<Room1>();
                break;
            // Code on face
            case PuzzlesEnum.Puzzle4:
                puzzleVariable = FindObjectOfType<LockedDoorPuzzle>();
                break;
            // Maze Puzzle
            case PuzzlesEnum.Puzzle5:
                puzzleVariable = FindObjectOfType<Room5>();
                break;
            // Mirrors Puzzle
            case PuzzlesEnum.Puzzle7:
                puzzleVariable = FindObjectOfType<Room2>();
                break;
            // Portraits Puzzle
            case PuzzlesEnum.Puzzle6:
                puzzleVariable = FindObjectOfType<Room6>();
                break;
            // Lamps puzzle
            case PuzzlesEnum.Puzzle9:
                puzzleVariable = FindObjectOfType<Room8>();
                break;
            // Final door puzzle
            case PuzzlesEnum.Puzzle11:
                puzzleVariable = FindObjectOfType<LockedDoorPuzzle>();
                break;
        }
    }

    /// <summary>
    /// Abstract method to execute a certain action
    /// Plays victory or gives a certain objet to the player
    /// Adds a puzzle done to PlayerGeneralInfo and writes to txt
    /// </summary>
    public override void Execute()
    {
        // Adds puzzle to player's puzzles done
        PlayerGeneralInfo playerInfo = FindObjectOfType<PlayerGeneralInfo>();
        playerInfo.PuzzlesDone |= puzzleName;

        // Writes puzzles done to a txt
        FileWriter writer = new FileWriter(FilePath.puzzlePath);
        writer.AddToTxt(playerInfo);

        // Does an action to complete the puzzle
        switch (assistModeType)
        {
            case AssistModeType.PlayVictory:
                if (puzzleVariable != null) base.ExecuteVictory(puzzleVariable);
                break;
            case AssistModeType.AddPrizeToInventory:
                base.AddPrizeToInventory();
                break;
        }
    }
}
