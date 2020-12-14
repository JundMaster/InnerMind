using UnityEngine;

/// <summary>
/// Class for bridge animation from room 9
/// </summary>
public class BridgeCubeAnimation : MonoBehaviour
{
    private Animator anim;

    /// <summary>
    /// Awake class for BridgeCubeAnimation
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// OnTriggerEnter for BridgeCubeAnimation. Sets a trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("Go Up");
    }

    /// <summary>
    /// OnTriggerExit for BridgeCubeAnimation. Sets a trigger
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        
        anim.SetTrigger("Go Down");
    }
}