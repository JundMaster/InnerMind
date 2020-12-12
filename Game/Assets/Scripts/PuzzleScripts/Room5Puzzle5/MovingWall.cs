using UnityEngine;

/// <summary>
/// Class responsible for each moving wall in puzzle5 Room5
/// </summary>
public class MovingWall : MonoBehaviour
{
    // Wall's animator on editor
    [SerializeField] private Animator wall;

    // Walls boxcollider on editor
    [SerializeField] private BoxCollider col;

    // Bool that defines the wall as an initial hiddenWall or not
    [SerializeField] private bool hiddenWall;

    // Bool to control if the wall plays the animation once or more times
    [SerializeField] private bool animateOnce;

    /// <summary>
    /// Awake method of MovingWall
    /// </summary>
    private void Awake()
    {
        if (hiddenWall) wall.SetTrigger("hide");
    }

    /// <summary>
    /// OnTriggerEnter of MovingWall
    /// Shows or hides the wall, depending if it's a hidden wall
    /// </summary>
    /// <param name="other">Collider the wall collided with</param>
    private void OnTriggerEnter(Collider other)
    {
        if (hiddenWall)
            wall.SetTrigger("show");
        else
            wall.SetTrigger("hide");
    }

    /// <summary>
    /// OnTriggerExit of MovingWall
    /// Shows or hides the wall, depending if it's a hidden wall
    /// </summary>
    /// <param name="other">Collider the wall collided with</param>
    private void OnTriggerExit(Collider other)
    {
        if (animateOnce == false)
        {
            if (hiddenWall)
                wall.SetTrigger("hide");
            else
                wall.SetTrigger("show");
        }
    }

    /// <summary>
    /// OnDrawGizmos of MovingWall. Draws spheres only on editor
    /// </summary>
    private void OnDrawGizmos()
    {
        if (hiddenWall)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + new Vector3(
                            col.center.x, 0f, col.center.z) , 0.5f);
    }
}
