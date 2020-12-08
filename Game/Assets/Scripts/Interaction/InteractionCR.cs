using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract class for interactions with a coroutine.
/// Extends InteractionCommon, for any interectable object
/// Implements ICoroutine for basic coroutines
/// </summary>
public abstract class InteractionCR : InteractionCommon, ICoroutine
{
    /// <summary>
    /// ThisCouroutine property. Used to have control of the coroutine
    /// </summary>
    public Coroutine ThisCoroutine { get; set;}

    /// <summary>
    /// This method determines the action of an object when clicked
    /// In this case, Execute calls CoroutineExecute
    /// </summary>
    public override void Execute()
    {
        if (ThisCoroutine == null)
            ThisCoroutine = StartCoroutine(CoroutineExecute());
    }

    /// <summary>
    /// This Coroutine determines the action of an object when clicked
    /// </summary>
    /// <returns>Returns null</returns>
    public abstract IEnumerator CoroutineExecute();
}