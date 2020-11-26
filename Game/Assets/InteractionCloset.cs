using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCloset : InteractionCommon
{
    Room1 room;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject keyPosition;
    private GameObject keyItem;
    private void Start()
    {
        room = FindObjectOfType<Room1>();
        keyItem = null;
        key.transform.localScale = new Vector3(5, 5, 5);
    }

    public override void Execute()
    {
        keyItem = Instantiate(key, keyPosition.transform.position, Quaternion.identity);
        room.Victory();
    }

    public override string ToString() => "Open Closet";
}
