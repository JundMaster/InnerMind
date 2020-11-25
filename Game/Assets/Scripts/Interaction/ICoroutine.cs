using System.Collections;
using UnityEngine;

public interface ICoroutine
{
    Coroutine ThisCoroutine { get; }
    IEnumerator CoroutineExecute();
}
