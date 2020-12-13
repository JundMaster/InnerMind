using System.Collections;
using UnityEngine;

/// <summary>
/// Class responsible for controlling a cube's parent in MirrorPuzzle
/// </summary>
public class MirrorPuzzleCubeParent : MonoBehaviour, ICoroutineT<LeftMiddleRight>
{
    /// <summary>
    /// ThisCouroutine property. Used to have control of the coroutine
    /// </summary>
    public Coroutine ThisCoroutine { get; private set; }

    // Correct position of this parent cubes
    [SerializeField] private LeftMiddleRight correctPosition;

    // Current position of this parent cubes
    [SerializeField] private LeftMiddleRight currentPosition;

    /// <summary>
    /// InCorrectPosition property. Bool to check if the cube is in correct pos
    /// </summary>
    public bool InCorrectPosition { get; private set; }

    // Variable for every cube in this object children
    private MirrorPuzzleCube[] cubes;

    // Components
    private Room2 room2;

    /// <summary>
    /// Awake method of MirrorPuzzleCubeParent
    /// </summary>
    private void Awake()
    {
        cubes = GetComponentsInChildren<MirrorPuzzleCube>();

        // Checks if the cubes are in correct position
        if (correctPosition == currentPosition)
            InCorrectPosition = true;
        else
            InCorrectPosition = false;

        room2 = FindObjectOfType<Room2>();
        ThisCoroutine = null;
    }

    /// <summary>
    /// OnEnable method of MirrorPuzzleCubeParent
    /// </summary>
    private void OnEnable()
    {
        // Subscribes to cube childs events
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].CubePosition += Move;
        }
    }

    /// <summary>
    /// OnDisable method of MirrorPuzzleCubeParent
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribes to cube childs events
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].CubePosition -= Move;
        }
    }

    /// <summary>
    /// If max position isn't reached, starts a coroutine to move the block
    /// </summary>
    /// <param name="dir">Receives direction to move the cube</param>
    private void Move(LeftMiddleRight dir)
    {
        // If the player hasn't done the puzzle yet
        if (room2.FinishedPuzzle == false)
        {
            // Compares current positions with desired positions
            switch (dir)
            {
                case LeftMiddleRight.Left:
                    if (currentPosition > LeftMiddleRight.Left)
                    {
                        // Plays sound and plays coroutine
                        if (ThisCoroutine == null)
                        {
                            SoundManager.PlaySound(SoundClip.WallSlide);

                            ThisCoroutine = StartCoroutine(
                                CoroutineExecute(LeftMiddleRight.Left));
                        }
                    }
                    else { }
                    break;
                case LeftMiddleRight.Right:
                    if (currentPosition < LeftMiddleRight.Right)
                    {
                        // Plays sound and plays coroutine
                        if (ThisCoroutine == null)
                        {
                            SoundManager.PlaySound(SoundClip.WallSlide);

                            ThisCoroutine = StartCoroutine(
                                CoroutineExecute(LeftMiddleRight.Right));
                        }
                    }
                    else { }
                    break;
            }
        }
    }

    /// <summary>
    /// This Coroutine determines the actions of an object. In this case 
    /// It smoothly moves a cube to the desired position
    /// </summary>
    /// <param name="value">Desired direction to move the cube</param>
    /// <returns>Returns null</returns>
    public IEnumerator CoroutineExecute(LeftMiddleRight dir)
    {
        float direction;
        // If direction is left, it says the direction is negative
        if (dir == LeftMiddleRight.Left)
        {
            currentPosition--;
            direction = -2;
        }
        // Else if direction is right, it says the direction is positive
        else
        {
            currentPosition++;
            direction = 2;
        }
        
        // New final position
        Vector3 desiredPosition = 
            transform.position + new Vector3(direction, 0, 0);

        // Moves the cube smoothly
        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                desiredPosition, elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Checks if the cube is in the right position
        if (correctPosition == currentPosition)
            InCorrectPosition = true;
        else
            InCorrectPosition = false;

        // After every actions, checks the puzzle's victory conditions
        room2.VictoryCheck();

        // Sets couroutine to null so it can be used again
        ThisCoroutine = null;
    }
}
