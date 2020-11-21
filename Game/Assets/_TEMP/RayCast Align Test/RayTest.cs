using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    private RaycastHit rayhit;
    private RaycastHit intObjectRayHit;

    private Vector3[] interactableObjectVertices;
    private Vector3[] finalObjectVertices;

    private Vector3 objectDistance;
    private Vector3 boundPoint1;
    private Vector3 boundPoint2;


    public float safeArea;

    private void Start()
    {

        Physics.Raycast(transform.position, transform.forward, out rayhit);

        //interactableObject = rayhit.transform.gameObject;

        Physics.Raycast(rayhit.transform.position, transform.forward, out intObjectRayHit);

        //finalObject = intObjectRayHit.transform.gameObject;

        intObjectRayHit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;

        objectDistance = rayhit.transform.position - intObjectRayHit.transform.position;

        safeArea = 0.3f;



    }

    private void Update()
    {
        interactableObjectVertices = GetObjectVertices(rayhit.transform.gameObject);
        finalObjectVertices = GetObjectVertices(intObjectRayHit.transform.gameObject);

        for (int i = 0; i < interactableObjectVertices.Length; i++)
        {
            interactableObjectVertices[i] -= objectDistance;
            for (int j = 0; j < finalObjectVertices.Length; j++)
            {

                if (isAligned(interactableObjectVertices[i], finalObjectVertices[j]))
                {
                    Debug.Log($"Pair of Vertices {i} / {j}");
                }

            }
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 100);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(rayhit.point, 0.1f);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(intObjectRayHit.point, 0.1f);

        Gizmos.color = Color.grey;
        foreach (Vector3 p in finalObjectVertices)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(p, Mathf.Pow(safeArea, 2));
        }

        foreach (Vector3 t in interactableObjectVertices)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(t, 0.1f);

        }
    }
    private Vector3[] GetObjectVertices(GameObject gameObject)
    {
        Vector3[] vertices;

        boundPoint1 = gameObject.GetComponent<MeshRenderer>().bounds.min;
        boundPoint2 = gameObject.GetComponent<MeshRenderer>().bounds.max;
        Vector3 boundPoint3 = new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint4 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z);
        Vector3 boundPoint5 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);
        Vector3 boundPoint6 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z);
        Vector3 boundPoint7 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint8 = new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z);

        vertices = new Vector3[] {
            boundPoint1,
            boundPoint2,
            boundPoint3,
            boundPoint4,
            boundPoint5,
            boundPoint6,
            boundPoint7,
            boundPoint8};

        return vertices;
    }

    private bool isAligned(Vector3 verticesObj1, Vector3 verticesObj2)
    {
        bool isAligned = false;

        if (Mathf.Pow((verticesObj1.x - verticesObj2.x), 2) +
            Mathf.Pow((verticesObj1.y - verticesObj2.y), 2) +
            Mathf.Pow((verticesObj1.z - verticesObj2.z), 2) < Mathf.Pow(safeArea, 2)) isAligned = true;

        return isAligned;

    }
}



