using UnityEngine;
using System.IO;

public class FileWriter
{
    private readonly string path;

    public FileWriter(string path)
    {
        this.path = path;
    }

    // Saves Items in inventory
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

    // Saves PuzzlesDone
    public void AddToTxt(PlayerGeneralInfo player)
    {
        using (StreamWriter sw = File.CreateText(path))
        {
            sw.WriteLine(player.PuzzlesDone);
        }
    }
}
