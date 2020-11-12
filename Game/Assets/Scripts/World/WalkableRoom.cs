using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class WalkableRoom : MonoBehaviour
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
        roomCollider.center = new Vector3(0, roomBounds.size.y / 2f, 0);
        roomCollider.size = new Vector3(roomBounds.size.x - 0.60f,
            roomBounds.size.y - 0.60f, roomBounds.size.z - 0.60f);
        roomCollider.isTrigger = true;
    }
}
