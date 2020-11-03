using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] private byte interactDistance; 

    // Components
    private PlayerInput input;
    private Camera mainCamera;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        mainCamera = Camera.main;
    }



    private void Update()
    {
        // Creates a ray from playerCamera to transform.forward
        Ray frontRay = new Ray(mainCamera.transform.position,
            mainCamera.transform.forward);

        if (input.LeftClick)
        {
            if (Physics.Raycast(frontRay, out RaycastHit hit, interactDistance))
            {
                if (hit.collider.gameObject.TryGetComponent
                    (out IInterectableController other))
                {
                    other.RunCoroutine();
                }
            }
        }

    }

}
