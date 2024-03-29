﻿using UnityEngine;

/// <summary>
/// Responsible for controlling the objets being examined and setting the
/// scene for it
/// </summary>
public class ItemExaminer
{
    // Speed of rotation 
    private readonly float speed;
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

    /// <summary>
    /// Constructor, that creates a new instance of ItemExaminer and 
    /// initializes its fields
    /// </summary>
    /// <param name="speed">Speen of rotation of the object</param>
    /// <param name="item">Object to be examined</param>
    /// <param name="examineCamera">Camera used to render the object</param>
    public ItemExaminer(float speed, ScriptableItem item, Camera examineCamera)
    {
        SetLights();
        SetExamineItem(item, examineCamera);
        // Set the speed rotation value
        this.speed = speed;
        verticalRotation = 0f;
        horizontalRotation = 0f;
    }

    /// <summary>
    /// Controlls the object in examine
    /// </summary>
    public void Examine()
    {
        IPlayerInput input = MonoBehaviour.FindObjectOfType<PlayerInput>();
        if (input.LeftClick)
        {
            verticalRotation = input.VerticalMouse * speed;
            horizontalRotation = input.HorizontalMouse * speed;
            itemTransform.Rotate(itemTransform.parent.up,
                                 -horizontalRotation,
                                 Space.World);

            itemTransform.Rotate(itemTransform.parent.right,
                                 -verticalRotation,
                                 Space.World);
        }
    }

    /// <summary>
    /// Sets the lights on the scene to eximine
    /// </summary>
    private void SetLights()
    {
        light = new GameObject("Light");
        lightComponent = light.AddComponent<Light>();
        lightComponent.type = LightType.Spot;
        lightComponent.range = 25f;
        lightComponent.intensity = 4;
    }

    /// <summary>
    /// Atributes the necessary properties to the item to be examined and
    /// positions it relatively to the camera that will render it
    /// </summary>
    /// <param name="item">Item to be examined</param>
    /// <param name="examineCamera">Camera that will render the item</param>
    private void SetExamineItem(ScriptableItem item, Camera examineCamera)
    {
        // Instantiates the item that player will interact with
        itemObject = MonoBehaviour.Instantiate(item.Prefab);
        itemObject.name = "Item In Examine";
        // Sets the object and its children to the layer that must be 
        // rendered by the camera

        RecursivelySetLayer(itemObject, 12);


        // Puts the item in front of the camera
        // The item's Z axis point towards the camera
        itemObject.transform.position = examineCamera.transform.position + 
                                        examineCamera.transform.forward;
        parent = new GameObject();
        parent.name = "Examine Set";
        itemTransform = itemObject.transform;
        // Makes the item parent position the same as the item position
        parent.transform.position = itemTransform.position;
        itemTransform.parent = parent.transform;
        light.transform.parent = parent.transform;
        // Points parent to the camera
        itemTransform.parent.LookAt(examineCamera.transform);
        light.transform.position = itemTransform.position;
        light.transform.position += itemTransform.parent.forward * 5;
        light.transform.position += itemTransform.parent.up * -4;
        light.transform.LookAt(itemTransform);
    }

    /// <summary>
    /// Recursively sets the layer of the item to be examined and all its
    /// children.
    /// </summary>
    /// <remarks>Some given items might have multiple children and 
    /// nested children. So the layer of those <see cref="GameObject"/>
    /// are set recursively.</remarks>
    /// <param name="itemObject">Item to be examined</param>
    /// <param name="layer">Layer that the object and children will be
    /// assign to</param>
    private void RecursivelySetLayer(GameObject itemObject, int layer)
    {
        itemObject.layer = layer;
        foreach (Transform child in itemObject.transform)
        {
            child.gameObject.layer = layer;
            if (child.transform.childCount > 0)
            {
                RecursivelySetLayer(child.gameObject, layer);
            }
        }
    }

    /// <summary>
    /// Stops the examining, destroying the <see cref="GameObject"/> objects
    /// created for it.
    /// </summary>
    public void StopExamine()
    {
        MonoBehaviour.Destroy(light);
        MonoBehaviour.Destroy(itemObject);
        MonoBehaviour.Destroy(parent);
    }


}
