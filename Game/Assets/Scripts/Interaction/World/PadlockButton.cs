using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for PadlockButton from door with code
/// </summary>
public class PadlockButton : MonoBehaviour
{
    // The door where the padlock is
    [SerializeField] private InteractionDoorWithCode padlockDoor;

    // Variables to get padlockwheels
    private GameObject[]    padlockWheelsGameObjects;
    private Transform[]     padlockWheels;

    // Variable to control coroutine
    private Coroutine coroutine;

    [SerializeField] private Button backButton;

    /// <summary>
    /// Start method for PadlockButton
    /// </summary>
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

    /// <summary>
    /// Update method for PadlockButton
    /// </summary>
    private void Update()
    {
        // If null, gets padlock wheels after the first time
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

        // If any of the wheels is rotating, the backButton is inactive
        if (coroutine != null)
            backButton.interactable = false;
        else
            backButton.interactable = true;
    }

    /// <summary>
    /// Method for first wheel
    /// Called when the button on the padlock is pressed
    /// </summary>
    /// <param name="result">Value of the wheel</param>
    public void FirstWheel(int result)
    {
        if (coroutine == null) 
            coroutine = StartCoroutine(Rotate(padlockWheels[0], result, 1));
    }

    /// <summary>
    /// Method for second wheel
    /// Called when the button on the padlock is pressed
    /// </summary>
    /// <param name="result">Value of the wheel</param>
    public void SecondWheel(int result)
    {
        if (coroutine == null)
            coroutine = StartCoroutine(Rotate(padlockWheels[1], result, 2));
    }

    /// <summary>
    /// Method for third wheel
    /// Called when the button on the padlock is pressed
    /// </summary>
    /// <param name="result">Value of the wheel</param>
    public void ThirdWheel(int result)
    {
        if (coroutine == null)
            coroutine = StartCoroutine(Rotate(padlockWheels[2], result, 3));
    }

    /// <summary>
    /// Rotates the wheel and changes Usercode value
    /// </summary>
    /// <param name="wheel">Wheel to rotate</param>
    /// <param name="dir">Direction to rotate the wheel to</param>
    /// <param name="wheelNumber">Wheel's number</param>
    /// <returns></returns>
    private IEnumerator Rotate(Transform wheel, int dir, int wheelNumber)
    {
        float elapsedTime = 0.0f;
        Quaternion from = wheel.transform.rotation;
        Quaternion to = wheel.transform.rotation;

        SoundManager.PlaySound(SoundClip.PadlockWheel);

        // If direction is negative, rotates the wheel to the left
        if (dir < 0)
            to *= Quaternion.Euler(0f, 36f, 0f);
        // Else rotates the wheel to the right
        else
            to *= Quaternion.Euler(0f, -36f, 0f);

        // Rotation animation
        while (elapsedTime < 1f)
        {
            wheel.transform.rotation = 
                Quaternion.Slerp(from, to, elapsedTime * 1.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Changes user code depending on the wheel's number
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

        // After all the operations
        // If the codes are equal, opens the door
        if (padlockDoor.UserCode == padlockDoor.DoorCode)
        {
            SoundManager.PlaySound(SoundClip.PadlockOpened);
            padlockDoor.OpenDoor();
        }

        // Sets the coroutine to null so it can be used again
        coroutine = null;
    }

    /// <summary>
    /// Called when the button back is pressed, while on padlock screen
    /// </summary>
    public void BackToGameplay()
    {
        // Only happens if the wheels are stopped
        if (coroutine == null)
            padlockDoor.BackToGameplay();
    }
}
