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
        roomCollider.size   = roomBounds.size;
        roomCollider.isTrigger = true;
    }
}
