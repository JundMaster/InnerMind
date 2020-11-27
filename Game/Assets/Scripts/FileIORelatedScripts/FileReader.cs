using UnityEngine;
using System.IO;

public class FileReader
{
    private readonly string path;

    public FileReader(string path)
    {
        this.path = path;
    }

    // Reads inventory items from txt
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

    public void ReadFromTXT(PlayerGeneralInfo player)
    {
        using (StreamReader sr = File.OpenText(path))
        {
            char[] charsToSplit = { ',', ' ' };
            string[] strs = sr.ReadLine().Split(charsToSplit);

            foreach (string str in strs)
            {
                if (PuzzlesEnum.TryParse(str, out PuzzlesEnum result))
                {
                    player.PuzzlesDone |= result;
                }
            }
        }
    }
}
