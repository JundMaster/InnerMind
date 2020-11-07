using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TypeOfRoom CurrentTypeOfRoom { get; set; } 
    
    private void Start()
    {
        CurrentTypeOfRoom = TypeOfRoom.NonWalkableWalls;
    }
}
