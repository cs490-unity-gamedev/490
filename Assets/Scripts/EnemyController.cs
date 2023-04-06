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

    private GameObject player;
    private Rigidbody2D rb; // reference for this object's Rigidbody2D
    private Vector2 movement;
    [SerializeField]
    private float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // use rb to manipulate mvm and rotation of object
        rb = this.GetComponent<Rigidbody2D>();

        // start shooting coroutine
        leftBulletTransform = gameObject.transform.GetChild(0).transform;
        rightBulletTransform = gameObject.transform.GetChild(1).transform;
        StartCoroutine(shoot(fireSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the direction our player is compared to our enemy obj
        if (player != null) {
            Vector3 direction = player.transform.position - transform.position;
            // calculate the angle btwn enemy obj and player obj
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // set RigidBody's rotation
            rb.rotation = angle;
            direction.Normalize(); // bc we want movement val to be btwn -1 and 1
            movement = direction;
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
}
