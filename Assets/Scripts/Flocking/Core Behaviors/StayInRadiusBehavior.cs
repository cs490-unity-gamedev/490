using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    //-----------------------------------------------------------------------
    // Adapted from "Flocking Algorithm in Unity" series
    // Author: Board To Bits Games, 2019
    //-----------------------------------------------------------------------

    [SerializeField] Vector2 center;
    [SerializeField] float radius = 15f;
    const float NINETY_PERCENT = 0.9f;

    public override Vector2 calculateMove(FlockUnit unit, List<Transform> context, Flock flock) {
        // based on how far away unit is from center, try to get it back to center
        Vector2 centerOffset = center - (Vector2)unit.transform.position;
        float radiusRatio = centerOffset.magnitude / radius;

        // check if unit is within 90% of the radius
        if (radiusRatio < NINETY_PERCENT) {
            return Vector2.zero;
        }
        // if unit is within 10% of the radius, pull it back towards center
        return centerOffset * radiusRatio * radiusRatio;
    }
}
