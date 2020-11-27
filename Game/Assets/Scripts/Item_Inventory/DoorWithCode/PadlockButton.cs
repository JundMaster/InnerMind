using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadlockButton : MonoBehaviour
{
    [SerializeField] private InteractionDoorWithCode padlockDoor;

    // Variables to get padlockwheels
    private GameObject[]    padlockWheelsGameObjects;
    private Transform[]     padlockWheels;

    // Running coroutine
    private Coroutine coroutine;

    private void Start()
    {
        // Gets padlock wheels for the first time
        padlockWheelsGameObjects = new GameObject[3];
        padlockWheels = new Transform[3];

        padlockWheelsGameObjects[0] =
            GameObject.FindGameObjectWithTag("FirstWheel");
        padlockWheelsGameObjects[1] =
            GameObject.FindGameObjectWithTag("SecondWheel");
        padlockWheelsGameObjects[2] =
            GameObject.FindGameObjectWithTag("ThirdWheel");
        for (int i = 0; i < padlockWheelsGameObjects.Length; i++)
        {
            padlockWheels[i] = 
                padlockWheelsGameObjects[i].GetComponent<Transform>();
        }
    }

    private void Update()
    {
        // Gets padlock wheels after the first time
        if (padlockWheelsGameObjects[0] == null)
        {
            padlockWheelsGameObjects[0] =
            GameObject.FindGameObjectWithTag("FirstWheel");
            padlockWheelsGameObjects[1] =
                GameObject.FindGameObjectWithTag("SecondWheel");
            padlockWheelsGameObjects[2] =
                GameObject.FindGameObjectWithTag("ThirdWheel");
            for (int i = 0; i < padlockWheelsGameObjects.Length; i++)
            {
                padlockWheels[i] =
                    padlockWheelsGameObjects[i].GetComponent<Transform>();
            }
        }
    }

    // Methods for the 3 wheels
    public void FirstWheel(int result)
    {
        if (coroutine == null) 
            coroutine = StartCoroutine(Rotate(padlockWheels[0], result, 1));
    }

    public void SecondWheel(int result)
    {
        if (coroutine == null)
            coroutine = StartCoroutine(Rotate(padlockWheels[1], result, 2));
    }

    public void ThirdWheel(int result)
    {
        if (coroutine == null)
            coroutine = StartCoroutine(Rotate(padlockWheels[2], result, 3));
    }

    // Rotates the wheel and changes Usercode value
    private IEnumerator Rotate(Transform wheel, int dir, int wheelNumber)
    {
        float elapsedTime = 0.0f;
        Quaternion from = wheel.transform.rotation;
        Quaternion to = wheel.transform.rotation;

        if (dir < 0)
            to *= Quaternion.Euler(0f, 36f, 0f);
        else
            to *= Quaternion.Euler(0f, -36f, 0f);

        // Rotate
        while (elapsedTime < 1f)
        {
            wheel.transform.rotation = 
                Quaternion.Slerp(from, to, elapsedTime * 1.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Change usercode value
        switch (wheelNumber)
        {
            case 1:
                if (dir > 0)
                {
                    if (padlockDoor.UserCode.x > 0)
                        padlockDoor.UserCode += new CustomVector3(-1, 0, 0);
                    else
                        padlockDoor.UserCode = new CustomVector3 (9,
                            padlockDoor.UserCode.y, padlockDoor.UserCode.z );
                }
                else
                {
                    if (padlockDoor.UserCode.x < 9)
                        padlockDoor.UserCode += new CustomVector3(1, 0, 0);
                    else
                        padlockDoor.UserCode = new CustomVector3 (0,
                            padlockDoor.UserCode.y, padlockDoor.UserCode.z );
                }
                break;
            case 2:
                if (dir > 0)
                {
                    if (padlockDoor.UserCode.y > 0)
                        padlockDoor.UserCode += new CustomVector3(0, -1, 0);
                    else
                        padlockDoor.UserCode = new CustomVector3(
                            padlockDoor.UserCode.x, 9, padlockDoor.UserCode.z);
                }
                else
                {
                    if (padlockDoor.UserCode.y < 9)
                        padlockDoor.UserCode += new CustomVector3(0, 1, 0);
                    else
                        padlockDoor.UserCode = new CustomVector3 (
                            padlockDoor.UserCode.x, 0, padlockDoor.UserCode.z);
                }
                break;
            case 3:
                if (dir > 0)
                {
                    if (padlockDoor.UserCode.z > 0)
                        padlockDoor.UserCode += new CustomVector3(0, 0, -1);
                    else
                        padlockDoor.UserCode = new CustomVector3(
                            padlockDoor.UserCode.x, padlockDoor.UserCode.y, 9);
                }
                else
                {
                    if (padlockDoor.UserCode.z < 9)
                        padlockDoor.UserCode += new CustomVector3(0, 0, 1);
                    else
                        padlockDoor.UserCode = new CustomVector3(
                            padlockDoor.UserCode.x, padlockDoor.UserCode.y, 0);
                }
                break;
        }

        // If the codes are equal, opens the door
        if (padlockDoor.UserCode == padlockDoor.DoorCode)
            padlockDoor.OpenDoor();

        coroutine = null;
    }

    public void BackToGameplay()
    {
        padlockDoor.BackToGameplay();
    }
}
