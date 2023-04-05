using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkScriptableObject : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomOnEachIteration = true;
}
