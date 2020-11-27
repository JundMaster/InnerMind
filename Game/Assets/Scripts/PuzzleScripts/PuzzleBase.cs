using System.Collections;
using UnityEngine;
using System.IO;
using System;

public abstract class PuzzleBase : MonoBehaviour, IPuzzle
{
    [SerializeField] protected PuzzlesEnum myPuzzle;

    protected PlayerGeneralInfo player;
    protected Inventory inventory;

    // Puzzle txt file
    private FileReader fileReader;

    protected Coroutine readPuzzlesDoneTxtCoroutine;

    private void Awake()
    {
        player = FindObjectOfType<PlayerGeneralInfo>();
        inventory = FindObjectOfType<Inventory>();

        // Reads the file after 1 second
        if (readPuzzlesDoneTxtCoroutine == null)
            readPuzzlesDoneTxtCoroutine = StartCoroutine(ReadPuzzlesDoneTxt());
    }

    // If a new scene was loaded, the player.PuzzlesDone
    // variable is updated with txt text containing puzzles done
    protected IEnumerator ReadPuzzlesDoneTxt()
    {
        yield return new WaitForSeconds(0.25f);
        // Reads txt with inventory info ( if it already exists )
        if (File.Exists(FilePath.puzzlePath))
        {
            fileReader = new FileReader(FilePath.puzzlePath);
            fileReader.ReadFromTXT(player);
        }

        if (player.PuzzlesDone.HasFlag(myPuzzle))
            Victory();

        yield return null;
    }

    public virtual void Victory()
    {
        player.PuzzlesDone |= myPuzzle;

        // Save puzzle to txt
        FileWriter fw = new FileWriter(FilePath.puzzlePath);
        fw.AddToTxt(player);
    }

    private void OnApplicationQuit()
    {
        File.Delete(FilePath.puzzlePath);
    }
}
