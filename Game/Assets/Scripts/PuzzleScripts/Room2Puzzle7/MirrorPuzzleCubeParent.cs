using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPuzzleCubeParent : MonoBehaviour
{
    public Coroutine thisCoroutine;

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

        thisCoroutine = null;
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
        if (room2.Victory == false)
        {
            switch (dir)
            {
                case LeftMiddleRight.Left:
                    if (currentPosition > LeftMiddleRight.Left)
                    {
                        if (thisCoroutine == null)
                            thisCoroutine = StartCoroutine(
                                CoroutineMove(LeftMiddleRight.Left));
                    }
                    else { }
                    break;
                case LeftMiddleRight.Right:
                    if (currentPosition < LeftMiddleRight.Right)
                    {
                        if (thisCoroutine == null)
                            thisCoroutine = StartCoroutine(
                                CoroutineMove(LeftMiddleRight.Right));
                    }
                    else { }
                    break;
            }
        }
    }

    // Changes current position and moves the block
    public IEnumerator CoroutineMove(LeftMiddleRight dir)
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

        room2.VictoryCondition();

        thisCoroutine = null;
    }
}
