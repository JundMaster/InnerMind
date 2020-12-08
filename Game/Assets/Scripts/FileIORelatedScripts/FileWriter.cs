using UnityEngine;
using System.IO;

/// <summary>
/// Class that writes files
/// </summary>
public class FileWriter
{
    private readonly string path;

    /// <summary>
    /// Constructor for FileWriter
    /// </summary>
    /// <param name="path">Receives a string to write the file to</param>
    public FileWriter(string path)
    {
        this.path = path;
    }

    /// <summary>
    /// Saves inventory items in a .txt
    /// </summary>
    /// <param name="Bag">Inventory's bag</param>
    public void AddToTxt(ObservableList<ScriptableItem> Bag)
    {
        using (StreamWriter sw = File.CreateText(path))
        {
            foreach (ScriptableItem item in Bag)
            {
                if (item != null)
                {
                    sw.WriteLine(item.ID);
                }
            }
        }
    }

    /// <summary>
    /// Writes puzzles done in a .txt
    /// </summary>
    /// <param name="player">PlayerGeneralInfo to know puzzles done 
    /// information</param>
    public void AddToTxt(PlayerGeneralInfo player)
    {
        using (StreamWriter sw = File.CreateText(path))
        {
            sw.WriteLine(player.PuzzlesDone);
        }
    }
}
