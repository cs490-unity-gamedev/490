using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehavior : SteeringBehavior
{
    [SerializeField] private float targetPlayerReachedThreshold = 0.5f;
    [SerializeField] private bool showGizmo = true;
    bool reachedLastTargetPlayer = true;

    // gizmo parameters
    private Vector2 targetPositionCached;
    private float[] interestsTemp;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, EnemyAIData enemyAIData)
    {
        // if we don't have a player, stop seeking
        // else, set a new player
        if (reachedLastTargetPlayer) {
            if (enemyAIData.targets == null || enemyAIData.targets.Count <= 0) {
                enemyAIData.currentTargetPlayer = null;
                return (danger, interest);
            } else {
                reachedLastTargetPlayer = false;
                enemyAIData.currentTargetPlayer = enemyAIData.targets.OrderBy(target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
            }
        }

        // cache the last position only if we still see the target (if the targets collection isn't empty)
        if (enemyAIData.currentTargetPlayer != null && enemyAIData.targets != null && enemyAIData.targets.Contains(enemyAIData.currentTargetPlayer)) {
            targetPositionCached = enemyAIData.currentTargetPlayer.position;
        }

        // first, check ir we have reached the target
        if (Vector2.Distance(transform.position, targetPositionCached) < targetPlayerReachedThreshold) {
            reachedLastTargetPlayer = true;
            enemyAIData.currentTargetPlayer = null;
            return (danger, interest);
        }

        // if we haven't yet reached the target, do the main logic of finding the interest directions
        Vector2 directionToTarget = (targetPositionCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++) {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            // accept only directions at the less than 90 degrees to the target direction
            if (result > 0) {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i]) {
                    interest[i] = valueToPutIn;
                }
            }
        }
        interestsTemp = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos() {
        if (showGizmo == false) {
            return;
        }
        Gizmos.DrawSphere(targetPositionCached, 0.2f);

        if (Application.isPlaying && interestsTemp != null) {
            if (interestsTemp != null) {
                Gizmos.color = Color.green;
                for (int i = 0; i < interestsTemp.Length; i++) {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestsTemp[i]);
                }
                if (reachedLastTargetPlayer == false) {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targetPositionCached, 0.1f);
                    print(targetPositionCached);
                }
            }
        }
    }
}
