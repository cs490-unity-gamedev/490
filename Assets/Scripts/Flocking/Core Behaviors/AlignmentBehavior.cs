using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FlockBehavior
{
    //-----------------------------------------------------------------------
    // Adapted from "Flocking Algorithm in Unity" series
    // Author: Board To Bits Games, 2019
    //-----------------------------------------------------------------------

    public override Vector2 calculateMove(FlockUnit unit, List<Transform> context, Flock flock) {
        // if no neighbors, maintain current alignment
        if (context.Count == 0) {
            return unit.transform.up;
        }

        // add all points together and average
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform item in context) {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
