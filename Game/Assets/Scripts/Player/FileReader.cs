using UnityEngine;
using System.IO;

public class FileReader
{
    private readonly string path;

    public FileReader(string path)
    {
        this.path = path;
    }

    public void ReadFromTXT(ObservableList<ScriptableItem> Bag,
        ScriptableItem[] possibleItems)
    {
        using (StreamReader sr = File.OpenText(path))
        {
            string str;

            // Reads line until last line
            while ((str = sr.ReadLine()) != null)
            {
                // If any item in the list is the same as the txt line
                foreach (ScriptableItem item in possibleItems)
                {
                    if (item.ID.ToString().Equals(str))
                        Bag.Add(item);
                }
            }
        }  
    }
}
