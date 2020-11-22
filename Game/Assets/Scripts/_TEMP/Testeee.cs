using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private ScriptableObject interaction;

    IInteract inter;

    public void Start()
    {
        inter = interaction as IInteract;
    }

    public void Execute()
    {
        inter.Execute();
    }
    
}
