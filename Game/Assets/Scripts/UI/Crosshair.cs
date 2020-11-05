using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    // Components
    private PlayerRays ray;
    private PlayerInteract interact;
    private Image crosshair;

    void Start()
    {
        ray = FindObjectOfType<PlayerRays>();
        interact = FindObjectOfType<PlayerInteract>();
        crosshair = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(ray.Forward, out RaycastHit hit,
                interact.InteractDistance))
        {
            if (hit.collider.gameObject.TryGetComponent
                (out IInterectableController other))
            {
                crosshair.color = Color.red;
            }
        }
        else
        {
            crosshair.color = Color.white;
        }
    }
}
