using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacleDetection : MonoBehaviour
{
    private EnemyController enemyController;
    private List<Collider2D> obstacles;

    void Start() {
        enemyController = GetComponentInParent<EnemyController>();
        obstacles = new List<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy") {
            obstacles.Add(col);
            enemyController.obstacleDetected = true;
            print("wall detected");
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        print("wall removed");
        obstacles.Remove(col);
        if (obstacles.Count == 0) {
            enemyController.obstacleDetected = false;
        }
    }
}
