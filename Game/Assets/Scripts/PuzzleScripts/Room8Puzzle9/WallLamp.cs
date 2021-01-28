using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// Responsible for controlling WallLamp objects
/// </summary>
public class WallLamp : MonoBehaviour
{
    [SerializeField]
    private int lampIndex;

    [SerializeField]
    private WallLampDirection solutionDirection;

    // List of the light of the lamp
    [SerializeField]
    private List<WallLampLight> lights;

    // List of the adjacent lamps with the definition of what side they are
    // in comparison to THIS lamp
    [SerializeField]
    private List<LampDirectionAssociation> lamps;

    /// <summary>
    /// Collection of Lights associated to the WallLamp
    /// </summary>
    public List<WallLampLight> Lights { get; private set; }

    private WallLampDirection currentDirection;

    /// <summary>
    /// Index of the wall lamp
    /// </summary>
    /// <remarks>
    /// The minimum value for this property should be 1
    /// </remarks>
    public int LampIndex { get; private set; }

    /// <summary>
    /// Instance of the class responsible for controlling the interaction
    /// of the WallLamp
    /// </summary>
    public WallLampInteraction InteractionController { get; private set; }

    /// <summary>
    /// Direction to which the wall lamp should be pointing in order to be
    /// considered as aligned
    /// </summary>
    public WallLampDirection SolutionDirection
    {
        get => solutionDirection;
        set
        {
            solutionDirection = value;
        }
    }

    /// <summary>
    /// Direction to which the wall lamp is pointing in the moment
    /// </summary>
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

    /// <summary>
    /// Event that is fired when the lamp rotates
    /// </summary>
    public event Action<int> LampRotated;

    /// <summary>
    /// Event that when the lamp is aligned
    /// </summary>
    public event Action LampAligned;
     
    /// <summary>
    /// Start method for WallLamp
    /// </summary>
    private void Start()
    {
        InteractionController = GetComponent<WallLampInteraction>();
        Lights = lights;
        LampIndex = lampIndex;
        CurrentDirection = WallLampDirection.Top;
    }

    /// <summary>
    /// Invokes the <see cref="LampRotated"/> event
    /// </summary>
    /// <param name="index">Index of the lamp that is firing the event</param>
    protected virtual void OnLampRotated(int index)
    {
        LampRotated?.Invoke(index);
    }

    /// <summary>
    /// Invokes the <see cref="LampAligned"/> event
    /// </summary>
    protected virtual void OnLampAligned()
    {
        LampAligned?.Invoke();
    }

    /// <summary>
    /// Checks the current state of the lamp and its adjacent lamps and sets
    /// its lights accordingly.
    /// </summary>
    /// <param name="checkAdjacentLights">Defines whether the adjacent lights
    /// should be checked</param>
    private void CheckLights(bool checkAdjacentLights)
    {
        // Checks if this lamp is aligned
        if (IsAligned())
        {
            int count = 0;
            // Loops through all the adjacent lapms
            foreach (LampDirectionAssociation adjacentLamp in lamps)
            {
                // Checks the aligment of the adjacent lights if the 
                // method is called with that 'checkAdjacentLights'
                // as true
                if (checkAdjacentLights)
                    adjacentLamp.lamp.CheckLights(checkAdjacentLights: false);
                
                // Checks if the current adjacent lamp is aligned
                if (adjacentLamp.lamp.IsAligned())
                {
                    count++;
                    for (int i = 0; i < Lights.Count; i++)
                    {
                        if (adjacentLamp.side == Lights[i].CurrentDirection)
                        {
                            SetLight(Lights[i], LightStates.On);
                        }
                    }
                }

                // In case the adjacent lamp is not aligned
                else
                {
                    // Lopps through all the lights
                    for (int i = 0; i < Lights.Count; i++)
                    {
                        // Check if the number of adjacent lamps is 1
                        if (lamps.Count == 1)
                        {
                            // The light pointing to the not aligned adjacent
                            // lamp will start blinking
                            if (Lights[i].CurrentDirection == 
                                adjacentLamp.side)
                            {
                                SetLight(Lights[i], LightStates.Blink);
                            }

                            // The light that is not pointing on the direction
                            // of the adjacent lamp will be turned on
                            else
                            {
                                SetLight(Lights[i], LightStates.On);
                            }
                        }
                        
                        // In case there is more then one adjacent light
                        else
                        {
                            // The light pointing to the not aligned adjacent
                            // lamp will start blinking
                            if (Lights[i].CurrentDirection == 
                                adjacentLamp.side)
                            {
                                SetLight(Lights[i], LightStates.Blink);
                            }
                        }
                        
                    }
                }
            }

            // If the lamps and the adjacent lamp are aligned
            if (count == lamps.Count)
            {
                // Set both lights of the lamp on
                SetLight(Lights[0], LightStates.On);
                SetLight(Lights[1], LightStates.On);
            }
        }

        // In case the lamp is not aligned
        else
        {
            // Loops through the adjacent lamps
            foreach (LampDirectionAssociation adjacentLamp in lamps)
            {
                // Checks if the adjacent lamps is aligned
                if (adjacentLamp.lamp.IsAligned())
                {
                    // Checks the aligment of the adjacent lights if the 
                    // method is called with that 'checkAdjacentLights'
                    // as true
                    if (checkAdjacentLights)
                        adjacentLamp.lamp.CheckLights(checkAdjacentLights: false);
                }
            }

            // If the lamp's current alignment is Top or Bottom, both its
            // lightis will blink
            if (CurrentDirection == WallLampDirection.Top ||
                CurrentDirection == WallLampDirection.Bottom)
            { 
                SetLight(Lights[0], LightStates.Blink);
                SetLight(Lights[1], LightStates.Blink);
            }

            
            else if (CurrentDirection == WallLampDirection.Right ||
                    CurrentDirection == WallLampDirection.Left)
            {
                // Loops through all the adjacent lamps
                foreach (LampDirectionAssociation lamp in lamps)
                {
                    // Loops throught the lights on THIS lamp
                    // this does not loop throught the lights of 
                    // the adjacent lamps
                    for (int i = 0; i < Lights.Count; i++)
                    {
                        // If the light is pointing on the direction of an
                        // adjacent lamp it will blink
                        if (Lights[i].CurrentDirection == lamp.side)
                        {
                            SetLight(Lights[i], LightStates.Blink);
                        }

                        // If there is only 1 adjacent lamps, both lights
                        // will be off
                        else if (lamps.Count == 1)
                        {
                            SetLight(Lights[i], LightStates.Off);
                        }
                    }
                }
            }

        }
    }

    /// <summary>
    /// Check whether the lamps is aligned
    /// </summary>
    /// <returns>Returns bool checking wheter the lamps is aligned</returns>
    public bool IsAligned() => CurrentDirection == SolutionDirection;
    
    /// <summary>
    /// Sets the given <see cref="WallLampLight"/> to a given 
    /// <see cref="LightStates"/>
    /// </summary>
    /// <param name="light">Light to be set</param>
    /// <param name="lightState">State to which the light will be set</param>
    private void SetLight(WallLampLight light, LightStates lightState)
    {
        switch (lightState)
        {
            case LightStates.On:
                light.LightComponent.range = 1.57f;
                light.LightComponent.color = Color.white;
                break;
            case LightStates.Off:
                light.LightComponent.range = 0;
                
                break;
            case LightStates.Blink:
                if (light != null && light.LightComponent != null)
                {
                    light.LightComponent.range = 1.57f;
                    light.LightComponent.color = Color.yellow;
                }
                break;
        }
    }

    /// <summary>
    /// Makes the lamp rotate
    /// </summary>
    public void RotateLamp()
    {
        CurrentDirection++;
        RotateLights();
        CheckLights(true);
        if (IsAligned())
        {
            OnLampAligned();
        }
    }
    
    /// <summary>
    /// Calls the chain rotation of the lamp
    /// </summary>
    public void ChainRotation()
    {
        RotateLamp();
        OnLampRotated(LampIndex);
        CheckLights(true);
        if (IsAligned())
        {
            OnLampAligned();
        }
    }

    /// <summary>
    /// Rotates the <see cref="WallLampLight"/> of the lamp
    /// </summary>
    private void RotateLights()
    {
        for (int i = 0; i < Lights.Count; i++)
        {
            Lights[i].CurrentDirection++;
        }
    }
}
