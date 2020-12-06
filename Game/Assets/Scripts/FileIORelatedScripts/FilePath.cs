using UnityEngine;

sealed public class FilePath
{
    public static readonly string puzzlePath = 
        Application.dataPath + "/puzzlesdone.txt";

    public static readonly string inventoryPath = 
        Application.dataPath + "/inventory.txt";

    public static readonly string lastScenePath =
        Application.dataPath + "/lastScene.txt";

    public static readonly string watchedCutscenes =
        Application.dataPath + "/watchedCutscenes.txt";
}
