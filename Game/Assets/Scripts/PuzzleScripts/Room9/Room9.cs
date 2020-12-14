
using UnityEngine;

public class Room9 : PuzzleBase
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private ScriptableItem pen;
    [SerializeField] private ScriptableItem walkmanBatteries;

    public void VictoryCheck()
    {
       
        print(inventory.Bag.Contains(pen));
        print(inventory.Bag.Contains(walkmanBatteries));
        if (inventory.Bag.Contains(pen) && 
                inventory.Bag.Contains(walkmanBatteries))
        {
            Victory();
        }
    }

    public override void Victory()
    {
        base.Victory();
        // After doing base.Victory, does aditional actions
        doorAnimator.SetTrigger("Open Door");
    }
}