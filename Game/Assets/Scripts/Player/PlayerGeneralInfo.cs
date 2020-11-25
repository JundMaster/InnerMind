using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneralInfo : MonoBehaviour
{
    [SerializeField] private TypeOfControl startingControl;
    [SerializeField] private TypeOfRoom startingRoom;

    private PlayerInput input;

    public PuzzlesEnum PuzzlesDone { get; set; }
    public TypeOfRoom CurrentTypeOfRoom { get; set; }
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        input.ChangeTypeOfControl(startingControl);
        CurrentTypeOfRoom = startingRoom;
        PuzzlesDone = PuzzlesEnum.Default;
        CurrentTypeOfRoom = TypeOfRoom.NonWalkableWalls;
    }

}
