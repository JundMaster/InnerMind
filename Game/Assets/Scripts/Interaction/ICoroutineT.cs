using System.Collections;
using UnityEngine;

/// <summary>
/// Basic interface for objects of type T with a coroutine
/// </summary>
public interface ICoroutineT<T>
{
    /// <summary>
    /// ThisCouroutine property. Used to have control of the coroutine
    /// </summary>
    Coroutine ThisCoroutine { get; }

    /// <summary>
    /// This Coroutine determines the actions of an object
    /// </summary>
    /// <param name="value">T value</param>
    /// <returns>Returns null</returns>
    IEnumerator CoroutineExecute(T value);
}
