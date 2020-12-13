using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for the entrance of the Room7
/// </summary>
public class Entrance : MonoBehaviour
{
    [SerializeField] Animator animator;
    /// <summary>
    /// OnTriggerEnter method for Entrance
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Start Rotation");
    }
}
