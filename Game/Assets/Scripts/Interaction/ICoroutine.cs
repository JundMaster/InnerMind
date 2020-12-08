using System.Collections;
using UnityEngine;

/// <summary>
/// Basic interface for objects with a coroutine
/// </summary>
public interface ICoroutine
{
    /// <summary>
    /// ThisCouroutine property. Used to have control of the coroutine
    /// </summary>
    Coroutine ThisCoroutine { get; }

    /// <summary>
    /// This Coroutine determines the actions of an object
    /// </summary>
    /// <returns>Returns null</returns>
    IEnumerator CoroutineExecute();
}
