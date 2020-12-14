using UnityEngine;

/// <summary>
/// Class for room9
/// </summary>
public class Room9 : PuzzleBase
{
    [SerializeField] private GameObject penGameObject;
    [SerializeField] private GameObject WalkmanBatteriesGameObject;

    [SerializeField] private ScriptableItem pen;
    [SerializeField] private ScriptableItem walkmanBatteries;

    /// <summary>
    /// OnEnable method for room9
    /// </summary>
    private void OnEnable()
    {
        ReadPuzzlesDone += DestroyPrizes;
    }
    
    /// <summary>
    /// OnDisable method for room 9
    /// </summary>
    private void OnDisable()
    {
        ReadPuzzlesDone -= DestroyPrizes;
    }

    /// <summary>
    /// If the player has any of the prizes, it destroyes that prize
    /// </summary>
    private void DestroyPrizes()
    {
        if (inventory.Bag.Contains(pen))
            Destroy(penGameObject);
        if (inventory.Bag.Contains(walkmanBatteries))
            Destroy(WalkmanBatteriesGameObject);
    }
}