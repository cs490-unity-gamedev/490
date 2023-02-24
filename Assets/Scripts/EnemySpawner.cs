using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int NORTH = 0;
    private const int EAST = 1;
    private const int SOUTH = 2;
    private const int WEST = 3;

    [SerializeField]
    private GameObject enemyPrefab;
    private List<Transform> spawnPoints = new List<Transform>();
    private Transform northSpawnPoint;
    private Transform eastSpawnPoint;
    private Transform southSpawnPoint;
    private Transform westSpawnPoint;

    [SerializeField]
    private float spawnInterval = 4f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints.Add(gameObject.transform.GetChild(0).gameObject.transform);
        spawnPoints.Add(gameObject.transform.GetChild(1).gameObject.transform);
        spawnPoints.Add(gameObject.transform.GetChild(2).gameObject.transform);
        spawnPoints.Add(gameObject.transform.GetChild(3).gameObject.transform);

        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        Transform spawnPoint = spawnPoints[Random.Range(NORTH,(WEST + 1))];
        Vector3 spawnPointPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f);
        Quaternion spawnPointRotation = spawnPoint.rotation;

        GameObject newEnemy = Instantiate(enemy, spawnPointPosition, spawnPointRotation);
        
        // this ensures that it will be an endless game (can add counter to stop spawn after some time)
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}