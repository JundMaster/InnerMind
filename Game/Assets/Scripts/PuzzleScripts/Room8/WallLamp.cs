using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class WallLamp : MonoBehaviour
{

    [SerializeField]
    private int lampIndex;

    [SerializeField]
    private WallLampDirection solutionDirection;

    [SerializeField]
    private List<WallLampLight> lights;

    [SerializeField]
    private List<LampDirectionAssociation> lamps;

    public int LampIndex { get; private set; }

    public WallLampInteraction interactionController { get; private set; }

    private WallLampDirection currentDirection;

    public WallLampDirection SolutionDirection
    {
        get => solutionDirection;
        set
        {
            solutionDirection = value;
        }
    }

    public WallLampDirection CurrentDirection
    {
        get => currentDirection;
        set
        {
            currentDirection = value;
            if (currentDirection > WallLampDirection.Right)
            {
                currentDirection = WallLampDirection.Top;
            }
        }
    }

    public event Action<int> LampRotated;
     
    private void Start()
    {
        interactionController = GetComponent<WallLampInteraction>();
        LampIndex = lampIndex;
        CurrentDirection = WallLampDirection.Top;
        CheckLights();
    }

    private void OnLampRotated(int index)
    {
        LampRotated?.Invoke(index);
    }

    private void CheckLights()
    {
        // Checks if this lamp is aligned
        if (IsAligned())
        {
            int count = 0;
            // Loops through all the adjacent lapms
            foreach (LampDirectionAssociation adjacentLamp in lamps)
            {
                // Checks if the adjacent lamps are aligned
                if (adjacentLamp.lamp.IsAligned())
                {
                    count++;
                }

                else
                {
                    //Debug.Log($"Lamp[{adjacentLamp.lamp.LampIndex}] is not aligned");
                    // Lopps through all the lights

                    for (int i = 0; i < lights.Count; i ++)
                    {
                        if (lights[i].CurrentDirection == adjacentLamp.direction)
                        {
                            //Debug.Log($"Lamp[{LampIndex}] will blink");
                            Debug.Log($"Lamp[{LampIndex}], {lights[i].Direction} Light will blink");
                        }
                        else
                        {
                            Debug.Log($"Lamp[{LampIndex}], {lights[i].Direction} on the {lights[i].CurrentDirection} Light will be active");
                        }
                    }
                }
            }
            if (count == lamps.Count)
            {
                Debug.Log($"Lamp[{LampIndex}] is aligned");
            }
        }
        else
        {
            bool startBlink = false;

            if (CurrentDirection == WallLampDirection.Top ||
                CurrentDirection == WallLampDirection.Bottom)
            {
                if (LampIndex % 2 == 0)
                {
                    startBlink = true;
                }
                else if (LampIndex % 2 != 0)
                {
                    Debug.Log($"Lamp[{LampIndex}] both lights will be off");
                }
            }
            else if (CurrentDirection == WallLampDirection.Right ||
                CurrentDirection == WallLampDirection.Left)
            {
                if (LampIndex % 2 != 0)
                {
                    startBlink = true;
                }
                else if (LampIndex % 2 == 0)
                {
                    Debug.Log($"Lamp[{LampIndex}] both lights will be off");
                }

            }
            if (startBlink && LampIndex % 2 != 0)
            {
                foreach(LampDirectionAssociation lamp in lamps)
                    for (int i = 0; i < lights.Count; i++)
                    {
                        if (lights[i].CurrentDirection == lamp.direction)
                        {
                            Debug.Log($"Lamp[{LampIndex}], light: {lights[i].Direction} on the {lights[i].CurrentDirection} will blink");
                        }
                        else
                        {
                            Debug.Log($"Lamp[{LampIndex}], light: {lights[i].Direction} on the {lights[i].CurrentDirection} will off");
                        }
                    }
            }
            else if (startBlink && LampIndex % 2 == 0)
            {
                Debug.Log($"Lamp[{LampIndex}], both lights will blink");
            }
        }
    }

    public bool IsAligned() => CurrentDirection == SolutionDirection;
    
    public void RotateLamp()
    {
        CurrentDirection++;
        RotateLights();
        CheckLights();
    }
    
    public void ChainRotation()
    {
        RotateLamp();
        OnLampRotated(LampIndex);
    }

    private void RotateLights()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].CurrentDirection++;
        }
    }
}
