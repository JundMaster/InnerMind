using System.IO;

/// <summary>
/// Class that reads files
/// </summary>
public class FileReader
{
    private readonly string path;

    /// <summary>
    /// Constructor for FileReader
    /// </summary>
    /// <param name="path">Receives a string to read the file from</param>
    public FileReader(string path)
    {
        this.path = path;
    }

    /// <summary>
    /// Reads inventory items from a .txt
    /// </summary>
    /// <param name="Bag">Inventory's bag</param>
    /// <param name="possibleItems">Possible items that exist in the game</param>
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

    /// <summary>
    /// Reads puzzles done from a .txt
    /// </summary>
    /// <param name="player">PlayerGeneralInfo to know puzzles done 
    /// information</param>
    public void ReadFromTXT(PlayerGeneralInfo player)
    {
        using (StreamReader sr = File.OpenText(path))
        {
            char[] charsToSplit = { ',', ' ' };
            string[] strs = sr.ReadLine().Split(charsToSplit);

            foreach (string str in strs)
            {
                // If puzzle is in PuzzlesEnum, returns result and adds it to
                // player.PuzzlesDone 
                if (PuzzlesEnum.TryParse(str, out PuzzlesEnum result))
                {
                    player.PuzzlesDone |= result;
                }
            }
        }
    }
}
