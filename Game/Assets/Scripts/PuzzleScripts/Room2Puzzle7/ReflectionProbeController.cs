using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionProbeController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private float positionZ = 35f;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    
    private void Update()
    {
        transform.position = new Vector3(playerMovement.transform.position.x,
            (positionZ - playerMovement.transform.position.z) / 6f, positionZ - playerMovement.transform.position.z / 2);
    }
}
