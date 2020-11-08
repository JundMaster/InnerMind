using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] private byte interactDistance; 
    public byte InteractDistance { get => interactDistance; }


    // Components
    private PlayerInput input;
    private PlayerRays ray;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        ray = GetComponent<PlayerRays>();
    }

    private void Update()
    {
        if (PlayerInput.CurrentControl == TypeOfControl.InGameplay)
        {
            if (input.LeftClick)
            {
                if (Physics.Raycast(ray.RayToMouse, out RaycastHit hit,
                    interactDistance))
                {
                    if (hit.collider.gameObject.TryGetComponent
                        (out IInteractable other))
                    {
                        other.Execute();
                    }
                }
            }
        }
    }
}
