using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetection : MonoBehaviour
{
    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            enemyController.targetPlayer = col.gameObject.transform;
        }
    }
}
