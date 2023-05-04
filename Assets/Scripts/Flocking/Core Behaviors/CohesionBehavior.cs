using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FlockBehavior
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
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context) {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count; // get middle point btwn all neighbors

        // create offset from unit position
        cohesionMove -= (Vector2)unit.transform.position;
        return cohesionMove;
    }
}
