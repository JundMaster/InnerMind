using System.Collections;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Abstract class responsible for creating puzzles in rooms.
/// Implements IPuzzle
/// </summary>
public abstract class PuzzleBase : MonoBehaviour, IPuzzle
{
    // Variable to define which puzzle is this in inspector
    [SerializeField] protected PuzzlesEnum myPuzzle;

    // Components
    protected PlayerGeneralInfo player;
    protected Inventory inventory;
    private FileReader fileReader;

    // Variable to control this corroutine
    protected Coroutine readPuzzlesDoneTxtCoroutine;

    /// <summary>
    /// Awake method for PuzzleBase
    /// </summary>
    private void Awake()
    {
        player = FindObjectOfType<PlayerGeneralInfo>();
        inventory = FindObjectOfType<Inventory>();

        // Reads the file after 0.25 seconds
        // This 0.25 delay is used as a safe precaution, so it always happens
        // after everything started in the room
        if (readPuzzlesDoneTxtCoroutine == null)
            readPuzzlesDoneTxtCoroutine = StartCoroutine(ReadPuzzlesDoneTxt());
    }

    /// <summary>
    /// If a new scene was loaded, the player.PuzzlesDone variable is updated
    /// with txt text containing puzzles done after 0.25 seconds
    /// </summary>
    /// <returns>Returns null</returns>
    protected IEnumerator ReadPuzzlesDoneTxt()
    {
        yield return new WaitForSeconds(0.25f);

        // Reads txt with inventory info ( if it already exists )
        if (File.Exists(FilePath.puzzlePath))
        {
            fileReader = new FileReader(FilePath.puzzlePath);
            fileReader.ReadFromTXT(player);
        }

        // If the player already has this puzzle done, the script runs Victory()
        if (player.PuzzlesDone.HasFlag(myPuzzle))
            Victory();

        OnReadPuzzlesDone();

        yield return null;
    }

    /// <summary>
    /// Does an action when the puzzle is solved
    /// </summary>
    public virtual void Victory()
    {
        // Adds mypuzzle to player's puzzles done
        player.PuzzlesDone |= myPuzzle;

        // Save puzzle to txt, so the txt will know which puzzles the player
        // has alreayd done
        FileWriter fw = new FileWriter(FilePath.puzzlePath);
        fw.AddToTxt(player);
    }

    /// <summary>
    /// Method to invoke ReadPuzzlesDone
    /// </summary>
    protected virtual void OnReadPuzzlesDone() => ReadPuzzlesDone?.Invoke();

    // Event to check if coroutine read puzzles done txt has ran
    public event Action ReadPuzzlesDone;
}
