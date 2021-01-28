using UnityEngine;

/// <summary>
/// Class for Room7Rotation
/// </summary>
public class Room7Rotation : MonoBehaviour
{
    public Animator anim { get; private set; }

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
        if (other.gameObject.CompareTag("Player"))
            anim.SetTrigger("Stop Rotation");
    }

    /// <summary>
    /// OnTriggerStay for Room7Rotation. Sets a trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            anim.SetTrigger("Stop Rotation");
    }

    /// <summary>
    /// OnTriggerStay for Room7Rotation. Sets a trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            anim.SetTrigger("Stop Rotation");
    }
}
