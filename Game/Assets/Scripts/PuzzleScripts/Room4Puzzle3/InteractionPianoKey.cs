using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InteractionPianoKey : InteractionCommon
{
    private Animator animator;
    [SerializeField] private PianoKeyID keyID;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Execute()
    {
        animator.SetTrigger("PlayKey");
        KeyID?.Invoke(keyID);
    }

    public event Action<PianoKeyID> KeyID;

    public override string ToString() => "PlayKey";

}
