using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2 : MonoBehaviour
{
    private MirrorPuzzleCubeParent[] cubeParentsInRoom;

    public bool Victory { get; private set; }

    private void Awake()
    {
        cubeParentsInRoom = GetComponentsInChildren<MirrorPuzzleCubeParent>();
        Victory = false;  
    }

    public void VictoryCondition()
    {
        sbyte counter;
        counter = 0;
        foreach (MirrorPuzzleCubeParent cubeParent in cubeParentsInRoom)
            if (cubeParent.InCorrectPosition)
                counter++;

        if (counter == cubeParentsInRoom.Length)
            Victory = true;

        Debug.Log(counter);
        Debug.Log(cubeParentsInRoom.Length);
    }
}
