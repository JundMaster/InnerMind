﻿using UnityEngine;

public class ItemExaminer
{
    // Speed of rotation
    private float speed;
    private Light lightComponent;
    private GameObject light;
    // Item that is being examined
    private GameObject itemObject;
    // Parent of the item
    private GameObject parent;
    // Transform of the item that is being examined
    private Transform itemTransform;
    private float verticalRotation;
    private float horizontalRotation;

    public ItemExaminer(float speed, ScriptableItem item, Camera examineCamera)
    {
        SetLights();
        SetExamineItem(item, examineCamera);
        // Set the speed rotation value
        this.speed = speed;
        verticalRotation = 0f;
        horizontalRotation = 0f;
    }

    public void Examine()
    {
        if (Input.GetButton("Fire1"))
        {
            verticalRotation = Input.GetAxis("Mouse Y") * speed;
            horizontalRotation = Input.GetAxis("Mouse X") * speed;
            itemTransform.Rotate(itemTransform.parent.up,
                                 -horizontalRotation,
                                 Space.World);

            itemTransform.Rotate(itemTransform.parent.right,
                                 -verticalRotation,
                                 Space.World);
        }
    }

    private void SetLights()
    {
        light = new GameObject("Light");
        lightComponent = light.AddComponent<Light>();
        lightComponent.type = LightType.Spot;
        lightComponent.range = 7.93f;
        lightComponent.intensity = 4;
    }

    private void SetExamineItem(ScriptableItem item, Camera examineCamera)
    {
        // Instantiates the item that player will interact with
        itemObject = MonoBehaviour.Instantiate(item.Prefab);
        itemObject.name = "Examined Item";
        // Sets the object to the layer that must be rendered by the camera
        itemObject.layer = 12;
        // Puts the item in front of the camera
        // The item's Z axis point towards the camera
        itemObject.transform.position = examineCamera.transform.position + examineCamera.transform.forward;
        // Attach parent to the item
        parent = new GameObject();
        itemTransform = itemObject.transform;
        // Makes the item parent position the same as the item position
        parent.transform.position = itemTransform.position;
        itemTransform.parent = parent.transform;
        // Points parent to the camera
        itemTransform.parent.LookAt(examineCamera.transform);
        light.transform.position = itemTransform.position;
        light.transform.position += itemTransform.parent.forward * 5;
        light.transform.position += itemTransform.parent.up * -4;
        light.transform.LookAt(itemTransform);
    }

    public void StopExamine()
    {
        MonoBehaviour.Destroy(itemObject);
        MonoBehaviour.Destroy(parent);
    }


}