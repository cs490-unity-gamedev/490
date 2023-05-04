using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : Detector
{
    //-----------------------------------------------------------------------
    // Adapted from "Context Steering AI" series
    // Author: Sunny Valley Studio, 2022
    //-----------------------------------------------------------------------

    [SerializeField] private float detectionRadius = 2;
    [SerializeField] private LayerMask layerMask; // layer mask representing the obstacles
    [SerializeField] private bool showGizmos = true;

    Collider2D[] colliders;

    public override void Detect(EnemyAIData enemyAIData) {
        // detect all colliders around `transform.position`
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);
        enemyAIData.obstacles = colliders;
    }

    private void OnDrawGizmos() {
        if (showGizmos == false) {
            return;
        }
        if (Application.isPlaying && colliders != null) {
            Gizmos.color = Color.red;
            foreach (Collider2D obstacleCollider in colliders) {
                Gizmos.DrawSphere(obstacleCollider.transform.position, 0.2f);
            }
        }
    }
}
