using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShape {}

[Serializable]
public class Cube : IShape
{
    public Vector3 size;
}

[Serializable]
public class Thing
{
    public int weight;
}
public class ConcreteInteraction : MonoBehaviour
{
    [SerializeReference] public IShape shape;

}