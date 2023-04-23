using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAvoidanceBehavior : SteeringBehavior
{
    //-----------------------------------------------------------------------
    // Adapted from "Context Steering AI" series
    // Author: Sunny Valley Studio
    //-----------------------------------------------------------------------

    [SerializeField] private float radius = 2f, agentColliderSize = 0.6f;
    [SerializeField] private bool showGizmo = true;
    // gizmo parameters
    float[] dangersResultTemp = null;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, EnemyAIData enemyAIData) {
        foreach (Collider2D wallCollider in enemyAIData.obstacles) {
            Vector2 directionToWall = wallCollider.ClosestPoint(transform.position) - (Vector2) transform.position;
            float distanceToWall = directionToWall.magnitude;

            // calculate weight based on the distance from Enemy to Wall
            float weight = distanceToWall <= agentColliderSize ? 1 : (radius - distanceToWall) / radius;
            Vector2 directionToWallNormalized = directionToWall.normalized;

            // add wall parameters to the danger array
            for (int i = 0; i < Directions.eightDirections.Count; i++) {
                // how close this direction is to the direction of wall
                float result = Vector2.Dot(directionToWallNormalized, Directions.eightDirections[i]);

                float valueToPutIn = result * weight;

                // override value only if it's higher than the current one stored in the danger array
                if (valueToPutIn > danger[i]) {
                    danger[i] = valueToPutIn;
                }
            }
        }
        dangersResultTemp = danger;
        return(danger, interest);
    }

    private void OnDrawGizmos() {
        if (showGizmo == false) {
            return;
        }

        if (Application.isPlaying && dangersResultTemp != null) {
            if (dangersResultTemp != null) {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangersResultTemp.Length; i++) {
                    Gizmos.DrawRay(
                        transform.position,
                        Directions.eightDirections[i] * dangersResultTemp[i]
                    );
                }
            }
        } else {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}

public static class Directions {
    public static List<Vector2> eightDirections = new List<Vector2>{
        new Vector2(0,1).normalized,
        new Vector2(1,1).normalized,
        new Vector2(1,0).normalized,
        new Vector2(1,-1).normalized,
        new Vector2(0,-1).normalized,
        new Vector2(-1,-1).normalized,
        new Vector2(-1,0).normalized,
        new Vector2(-1,1).normalized
    };
}
