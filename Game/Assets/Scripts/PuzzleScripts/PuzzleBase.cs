using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour, IPuzzle
{
    [SerializeField] protected PuzzlesEnum myPuzzle;

    protected PlayerGeneralInfo player;
    protected Inventory inventory;

    private void Awake()
    {
        player = FindObjectOfType<PlayerGeneralInfo>();
        inventory = FindObjectOfType<Inventory>();
    }

    public virtual void Victory()
    {
        FindObjectOfType<PlayerGeneralInfo>().PuzzlesDone |= myPuzzle;
    } 
}
