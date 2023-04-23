using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSolver : MonoBehaviour
{
    //-----------------------------------------------------------------------
    // Adapted from "Context Steering AI" series
    // Author: Sunny Valley Studio
    //-----------------------------------------------------------------------

    [SerializeField] private bool showGizmos = true;

    // gizmo parameters
    float[] interestGizmo = new float[0];
    Vector2 resultDirection = Vector2.zero;
    private float rayLength = 1;
    const int NUM_DIRS = 8;

    private void Start() {
        interestGizmo = new float[8];
    }

    public Vector2 GetDirectionToMove(List<SteeringBehavior> behaviors, EnemyAIData enemyAIData) {
        float[] danger = new float[8];
        float[] interest = new float[8];

        // loop through each behavior
        foreach (SteeringBehavior behavior in behaviors) {
            (danger, interest) = behavior.GetSteering(danger, interest, enemyAIData);
        }

        // subtract danger values from interest array
        for (int i = 0; i < NUM_DIRS; i++) {
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
        }

        interestGizmo = interest;

        // get the average direction
        Vector2 outputDirection = Vector2.zero;
        for (int i = 0; i < NUM_DIRS; i++) {
            outputDirection += Directions.eightDirections[i] * interest[i];
        }
        outputDirection.Normalize();

        resultDirection = outputDirection;

        // return the selected movement direction
        return resultDirection;
    }

    private void OnDrawGizmos() {
        if (Application.isPlaying && showGizmos) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, resultDirection * rayLength);
        }
    }
}
