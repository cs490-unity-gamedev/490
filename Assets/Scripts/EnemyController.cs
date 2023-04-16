using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static event System.Action onEnemyDeath;
    [SerializeField]
    private int health = 3;
    [SerializeField]
    private float fireSpeed;
    [SerializeField]
    private GameObject bulletPrefab;
    private Transform leftBulletTransform;
    private Transform rightBulletTransform;
    private Transform playerDetectionCollider;
    private Transform obstacleDetectionCollider;

    private GameObject player;
    private Rigidbody2D rb; // reference for this object's Rigidbody2D
    private Vector2 movement;
    [SerializeField]
    private float moveSpeed = 1f;
    public bool obstacleDetected = false;
    public Transform playerPosition = null;
    private float patrolTimer = 0f;
    private float patrolTimeLimit = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // use rb to manipulate mvm and rotation of object
        rb = this.GetComponent<Rigidbody2D>();
        
        playerDetectionCollider = gameObject.transform.GetChild(0).transform;
        obstacleDetectionCollider = gameObject.transform.GetChild(1).transform;

        // start shooting coroutine
        leftBulletTransform = gameObject.transform.GetChild(2).transform;
        rightBulletTransform = gameObject.transform.GetChild(3).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition != null) {
            // ==== DETECTED PLAYER BEHAVIOR ====
            StartCoroutine(shoot(fireSpeed));
            // calculate the direction our player is compared to our enemy obj
            Vector3 direction = player.transform.position - transform.position;
            // calculate the angle btwn enemy obj and player obj
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // set RigidBody's rotation
            rb.rotation = angle;
            direction.Normalize(); // bc we want movement val to be btwn -1 and 1
            movement = direction;
        } else {
            // ==== PATROL BEHAVIOR ====

            // transform.position += transform.right * moveSpeed * Time.deltaTime;

            if (obstacleDetected) {
                // turn around
                // obstacleDetected = false;
                Vector3 newDir = Direction.randomDir();
                transform.eulerAngles = newDir;
            } else {
                transform.position += transform.right * moveSpeed * Time.deltaTime;
            }
        }
    }

    private void tickPatrolTimer() {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolTimeLimit) {
            patrolTimer = 0;
        }
    }

    private void FixedUpdate() {
        moveCharacter(movement);
    }

    public void takeDamage(int damage) {
        health -= damage;
        
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        // could instantiate an explosion animation here later
        Destroy(gameObject);
        // invoke to increase player score
        onEnemyDeath?.Invoke();
        // logic.addScore(1);
    }

    private void moveCharacter(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private IEnumerator shoot(float interval) {
        yield return new WaitForSeconds(interval);

        Instantiate(bulletPrefab, leftBulletTransform.position, leftBulletTransform.rotation);
        Instantiate(bulletPrefab, rightBulletTransform.position, rightBulletTransform.rotation);

        StartCoroutine(shoot(interval));
    }

    public static class Direction {
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
}
