using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInterectableController : IInteractable
{
    Coroutine ThisCoroutine { get; }
    bool CR_RunningCoroutine { get; }
    void RunCoroutine();
}
