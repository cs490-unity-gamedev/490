using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    //-----------------------------------------------------------------------
    // Adapted from "Flocking Algorithm in Unity" series
    // Author: Board To Bits Games
    //-----------------------------------------------------------------------
    public abstract Vector2 calculateMove(FlockUnit unit, List<Transform> context, Flock flock);
}
