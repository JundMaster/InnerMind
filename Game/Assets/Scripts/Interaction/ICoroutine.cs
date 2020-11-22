using System.Collections;
using UnityEngine;

public interface ICoroutine
{
    Coroutine ThisCoroutine { get; set; }
    IEnumerator CoroutineInteraction();
}
