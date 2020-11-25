using System.Collections;
using UnityEngine;

public class MirrorPuzzleCubeParent : MonoBehaviour, ICoroutineT<LeftMiddleRight>
{
    public Coroutine ThisCoroutine { get; private set; }

    // Correct position to solve puzzle
    [SerializeField] private LeftMiddleRight correctPosition;

    // Current position
    [SerializeField] private LeftMiddleRight currentPosition;

    public bool InCorrectPosition { get; private set; }

    private MirrorPuzzleCube[] cubes;

    private Room2 room2;
    private void Awake()
    {
        cubes = GetComponentsInChildren<MirrorPuzzleCube>();

        if (correctPosition == currentPosition)
            InCorrectPosition = true;
        else
            InCorrectPosition = false;

        room2 = FindObjectOfType<Room2>();

        ThisCoroutine = null;
    }

    // On cube interaction, calls Move method
    private void OnEnable()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].CubePosition += Move;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].CubePosition -= Move;
        }
    }

    // If max position isn't reached, starts a coroutine to move the block
    private void Move(LeftMiddleRight dir)
    {
        if (room2.FinishedPuzzle == false)
        {
            switch (dir)
            {
                case LeftMiddleRight.Left:
                    if (currentPosition > LeftMiddleRight.Left)
                    {
                        if (ThisCoroutine == null)
                            ThisCoroutine = StartCoroutine(
                                CoroutineExecute(LeftMiddleRight.Left));
                    }
                    else { }
                    break;
                case LeftMiddleRight.Right:
                    if (currentPosition < LeftMiddleRight.Right)
                    {
                        if (ThisCoroutine == null)
                            ThisCoroutine = StartCoroutine(
                                CoroutineExecute(LeftMiddleRight.Right));
                    }
                    else { }
                    break;
            }
        }
    }

    // Changes current position and moves the block
    public IEnumerator CoroutineExecute(LeftMiddleRight dir)
    {
        float direction;
        if (dir == LeftMiddleRight.Left)
        {
            currentPosition--;
            direction = -2;
        }
        else
        {
            currentPosition++;
            direction = 2;
        }
        
        Vector3 desiredPosition = transform.position + new Vector3(direction, 0, 0);

        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                desiredPosition, elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (correctPosition == currentPosition)
            InCorrectPosition = true;
        else
            InCorrectPosition = false;

        room2.VictoryCheck();

        ThisCoroutine = null;
    }
}
