using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    //-----------------------------------------------------------------------
    // Adapted from "Context Steering AI" series
    // Author: Sunny Valley Studio
    //-----------------------------------------------------------------------

    [SerializeField] private List<SteeringBehavior> steeringBehaviors;
    [SerializeField] private List<Detector> detectors;
    [SerializeField] private EnemyAIData enemyAIData;
    [SerializeField] private float detectionDelay = 0.05f;

    [SerializeField] private ContextSolver movementDirectionSolver;
    private EnemyController enemyController;
    private Vector2 movement;
    

    // inputs sent from the Enemy AI to the Enemy controller
    public UnityEvent OnAttackPressed;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    private void Start() {
        enemyController = GetComponentInParent<EnemyController>();
        // detecting player and walls around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection() {
        foreach (Detector detector in detectors) {
            detector.Detect(enemyAIData);
        }
    }

    private void Update() {
        // enemy AI movement based on target player availability
        if (enemyAIData.currentTargetPlayer != null) {
            if (gameObject.tag == "Enemy") {
                // don't shoot if current gameObject is Flocking Units Manager
                enemyController.shoot();
            }
            movement = movementDirectionSolver.GetDirectionToMove(steeringBehaviors, enemyAIData);
        } 
        else if (enemyAIData.GetTargetsCount() > 0) {
            // target acquisition logic
            enemyAIData.currentTargetPlayer = enemyAIData.targets[0];
        }
        else {
            enemyController.patrol();
        }
    }


    private void FixedUpdate() {
        if (enemyAIData.currentTargetPlayer != null) {
            enemyController.chasePlayer(movement, enemyAIData.currentTargetPlayer);
        }
    }
}

public static class RotationAngles {
    public static List<Vector3> dirs = new List<Vector3> {
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 45),
        new Vector3(0, 0, 90),
        new Vector3(0, 0, 135),
        new Vector3(0, 0, 180),
        new Vector3(0, 0, 225),
        new Vector3(0, 0, 270),
        new Vector3(0, 0, 315),
    };

    public static Vector3 randomDir() {
        return dirs[UnityEngine.Random.Range(0, dirs.Count)];
    }
}
