using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class WalkableRoom : MonoBehaviour
{
    private BoxCollider roomCollider;

    void Start()
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
        roomCollider.center = roomBounds.center;
        roomCollider.size = new Vector3(roomBounds.size.x - 0.60f,
            roomBounds.size.y - 0.60f, roomBounds.size.z - 0.60f);
        roomCollider.isTrigger = true;
    }
}
