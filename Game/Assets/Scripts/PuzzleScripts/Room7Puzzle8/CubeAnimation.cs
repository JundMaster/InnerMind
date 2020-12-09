using UnityEngine;

/// <summary>
/// Class for cube animation from room 7 puzzle 8
/// </summary>
public class CubeAnimation : MonoBehaviour
{
    private Animator anim;

    /// <summary>
    /// Awake class for CubeAnimation
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// OnTriggerEnter for CubeAnimation. Sets a trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("Go Up");
    }
}
