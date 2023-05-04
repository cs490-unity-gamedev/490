using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingObstacleDetection : MonoBehaviour
{
    private EnemyAI enemyAI;
    private List<Collider2D> obstacles;
    public bool obstacleDetectedWhilePatrolling = false;

    void Awake() {
        obstacles = new List<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy") {
            obstacles.Add(col);
            obstacleDetectedWhilePatrolling = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy") {
            obstacles.Remove(col);
        }
        if (obstacles.Count == 0) {
            obstacleDetectedWhilePatrolling = false;
        }
    }
}
