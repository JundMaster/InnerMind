using UnityEngine;
using System.IO;

public class FileWriter
{
    private readonly string path;

    public FileWriter(string path)
    {
        this.path = path;
    }

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
}
