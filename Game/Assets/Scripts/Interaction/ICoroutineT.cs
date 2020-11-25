using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoroutineT<T>
{
    Coroutine ThisCoroutine { get; }
    IEnumerator CoroutineExecute(T value);
}
