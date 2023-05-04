using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : Detector
{
    //-----------------------------------------------------------------------
    // Adapted from "Context Steering AI" series
    // Author: Sunny Valley Studio, 2022
    //-----------------------------------------------------------------------

    [SerializeField] private float playerDetectionRange = 5;
    [SerializeField] private LayerMask wallsLayerMask, playerLayerMask;
    [SerializeField] private bool showGizmos = false;

    // gizmo parameters
    private List<Transform> colliders;

    public override void Detect(EnemyAIData enemyAIData)
    {
        // Find out if player is near
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, playerDetectionRange, playerLayerMask);

        if (playerCollider != null) {
            // check if player is seen
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, playerDetectionRange, wallsLayerMask);

            // make sure that collider we see is on the "Player" layer
            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0) {
                // Debug.DrawRay(transform.position, direction * playerDetectionRange, Color.magenta);
                colliders = new List<Transform>() { playerCollider.transform };
            } else {
                colliders = null;
            }
        } else {
            // enemy doesn't see player
            colliders = null;
        }
        enemyAIData.targets = colliders;
    }

    private void OnDrawGizmosSelected() {
        if (showGizmos == false) {
            return;
        }

        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);

        if (colliders == null ) {
            return;
        }
        Gizmos.color = Color.magenta;
        foreach (var item in colliders) {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}
