using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for executing the interaction between player - lamps and the
/// lamps with each other
/// </summary>
public class WallLampInteraction : InteractionCR
{
    // Defines whether the interaction is still running
    private bool onInteraction;
    private bool interacting;

    public bool CanInteract { get; set; }
    // Reference to the lamp
    private WallLamp lamp;

    /// <summary>
    /// Start method for WallLampInteraction
    /// </summary>
    private void Start()
    {
        onInteraction = false;
        interacting = false;
        CanInteract = true;
        lamp = GetComponent<WallLamp>();
    }

    /// <summary>
    /// This Coroutine determines the action of the wall lamp when clicked
    /// </summary>
    /// <returns>Returns null</returns>
    public override IEnumerator CoroutineExecute()
    {
        StartCoroutine(RotationInteraction());
        yield break;
    }

    /// <summary>
    /// This Coroutine determines the reaction of the lamp when another is 
    /// interact to
    /// </summary>
    /// <returns>Returns null</returns>
    public IEnumerator ChainRotationExecute()
    {
        if (RotationInteractionCommon() != null)
            StartCoroutine(RotationInteractionCommon());
        yield break;
    }

    /// <summary>
    /// Interaction that will happen when the wall lamp is interact to
    /// </summary>
    /// <returns>Returns null</returns>
    private IEnumerator Rotate()
    {
        float elapsedTime = 0f;
        float timeLimit = 0.5f;
        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.LookRotation(transform.forward, -transform.right);

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && onInteraction || CanInteract == false)
            yield break;

        SoundManager.PlaySound(SoundClip.WallLamp);
        onInteraction = true;
        while (elapsedTime < timeLimit)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsedTime * 2.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        onInteraction = false;
    }

    /// <summary>
    /// Executes the rotation of the lamps and all the lamps that should
    /// be affected by it
    /// </summary>
    /// <returns>Returns null</returns>
    private IEnumerator RotationInteraction()
    {
        StartCoroutine(Rotate());

        float elapsedTime = 0f;
        float timeLimit = 0.5f;

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && interacting)
            yield break;

        interacting = true;
        while (elapsedTime < timeLimit)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        interacting = false;

        lamp.ChainRotation();
        yield break;
    } 
    
    /// <summary>
    /// Executes the rotation of the lamps
    /// </summary>
    /// <remarks>
    /// This interaction does not affect any other lamp
    /// </remarks>
    /// <returns>Returns null</returns>
    private IEnumerator RotationInteractionCommon()
    {
        StartCoroutine(Rotate());

        float elapsedTime = 0f;
        float timeLimit = 0.5f;

        // Verification to avoid click spam
        if (elapsedTime < timeLimit && interacting)
            yield break;

        interacting = true;
        while (elapsedTime < timeLimit)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        interacting = false;

        lamp.RotateLamp();
        yield break;
    }

    public override string ToString()
    {
        return "Rotate lamp";
    }
}