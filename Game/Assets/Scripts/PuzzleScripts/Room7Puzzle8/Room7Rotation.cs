using UnityEngine;
using System.Collections;

/// <summary>
/// Class for Room7Rotation
/// </summary>
public class Room7Rotation : MonoBehaviour
{

    private Animator anim;

    /// <summary>
    /// Start method for Room7Rotation
    /// </summary>
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// OnTriggerEnter for Room7Rotation. Sets a trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("Stop Rotation");
    }
}
