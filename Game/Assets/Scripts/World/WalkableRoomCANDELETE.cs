using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class WalkableRoomCANDELETE : MonoBehaviour
{
    private BoxCollider roomCollider;

    private void Start()
    {
        roomCollider = GetComponent<BoxCollider>();

        // Creates a bounds variable
        Bounds roomBounds = new Bounds(transform.position, Vector3.zero);

        // Creates bounds based on every renderer inside the parent object
        foreach (Renderer obj in GetComponentsInChildren<Renderer>())
        {
            roomBounds.Encapsulate(obj.bounds);
        }

        // Sets the collider with the new measures
        roomCollider.center = new Vector3(0f, 0f, 0f);
        roomCollider.size = new Vector3(roomBounds.size.x - 1.5f,
            roomBounds.size.y - 1.5f, roomBounds.size.z - 1.5f);
        roomCollider.isTrigger = true;
    }
}
