using System.Collections;
using UnityEngine;
using System.IO;

public abstract class PuzzleBase : MonoBehaviour, IPuzzle
{
    [SerializeField] protected PuzzlesEnum myPuzzle;

    protected PlayerGeneralInfo player;
    protected Inventory inventory;

    // Puzzle txt file
    private FileReader fileReader;

    private Coroutine readPuzzlesDoneTxtCoroutine;

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
    private IEnumerator ReadPuzzlesDoneTxt()
    {
        yield return new WaitForSeconds(1f);
        // Reads txt with inventory info ( if it already exists )
        if (File.Exists(FilePath.puzzlePath))
        {
            fileReader = new FileReader(FilePath.puzzlePath);
            fileReader.ReadFromTXT(player);
        }
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
