using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private Animator wall;
    [SerializeField] private BoxCollider col;

    [SerializeField] private bool hiddenWall;
    [SerializeField] private bool animateOnce;

    private void Awake()
    {
        if (hiddenWall) wall.SetTrigger("hide");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hiddenWall)
            wall.SetTrigger("show");
        else
            wall.SetTrigger("hide");
    }

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

    private void OnDrawGizmos()
    {
        if (hiddenWall)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + new Vector3(col.center.x, 0f, col.center.z) , 0.5f);
    }
}
