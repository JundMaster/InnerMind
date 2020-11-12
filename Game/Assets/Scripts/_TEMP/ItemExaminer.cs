using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExaminer
{
    // Speed of rotation
    private float speed;
    // Item that is being examined
    private GameObject itemObject;
    // Parent of the item
    private GameObject parent;
    // Transform of the item that is being examined
    private Transform itemTransform;
    private float verticalRotation;
    private float horizontalRotation;

    public ItemExaminer(float speed, ScriptableItem item)
    {
        // Set the speed rotation value
        this.speed = speed;

        // Instantiates the item that player will interact with
        itemObject = MonoBehaviour.Instantiate(item.Prefab);

        // Puts the item in front of the camera
        // The item's Z axis point towards the camera
        itemObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward;

        // Attach parent to the item
        parent = new GameObject();

        itemTransform = itemObject.transform;

        // Makes the item parent position the same as the item position
        parent.transform.position = itemTransform.position;
        itemTransform.parent = parent.transform;

        verticalRotation = 0f;
        horizontalRotation = 0f;

        // Points parent to the camera
        itemTransform.parent.LookAt(Camera.main.transform);
    }

    public void Examine()
    {
        if (Input.GetButton("Fire1"))
        {
            verticalRotation = Input.GetAxis("Mouse Y") * speed;
            horizontalRotation = Input.GetAxis("Mouse X") * speed;
            Cursor.lockState = CursorLockMode.Locked;

            itemTransform.Rotate(itemTransform.parent.up,
                                 -horizontalRotation,
                                 Space.World);

            itemTransform.Rotate(itemTransform.parent.right,
                                 -verticalRotation,
                                 Space.World);
        }
    }

    public void StopExamine()
    {
        MonoBehaviour.Destroy(itemObject);
        MonoBehaviour.Destroy(parent);
    }


}
