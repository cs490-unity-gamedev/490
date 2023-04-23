using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //-----------------------------------------------------------------------
    // Adapted from "Procedural Dungeon Generation in Unity 2d" series
    // Author: Sunny Valley Studios
    //-----------------------------------------------------------------------

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkScriptableObject : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomOnEachIteration = true;
}
