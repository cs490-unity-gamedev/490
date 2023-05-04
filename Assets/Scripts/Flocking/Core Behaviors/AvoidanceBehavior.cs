using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{
    //-----------------------------------------------------------------------
    // Adapted from "Flocking Algorithm in Unity" series
    // Author: Board To Bits Games, 2019
    //-----------------------------------------------------------------------

    public override Vector2 calculateMove(FlockUnit unit, List<Transform> context, Flock flock) {
        // if no neighbors, return no adjustment
        if (context.Count == 0) {
            return Vector2.zero;
        }

        // add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int numInAvoidRadius = 0;
        foreach (Transform item in context) {
            if (Vector2.SqrMagnitude(item.position - unit.transform.position) < flock.squareAvoidanceRadius) {
                numInAvoidRadius++;
                avoidanceMove += (Vector2)(unit.transform.position - item.position);
            }
        }
        if (numInAvoidRadius > 0) {
            avoidanceMove /= numInAvoidRadius;
        }

        return avoidanceMove;
    }
}
