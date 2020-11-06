using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInterectableController : IInteractable
{
    Coroutine ThisCoroutine { get; }
    void RunCoroutine();
}
