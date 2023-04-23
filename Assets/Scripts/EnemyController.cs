using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyController : MonoBehaviour
{
    public static event System.Action onEnemyDeath;
    [SerializeField] private int health = 3;
    [SerializeField] private float fireSpeed = 0.5f;
    [SerializeField] private GameObject bulletPrefab;
    private Transform bulletTransform;
    PatrollingObstacleDetection patrollingObstacleDetection;

    private Rigidbody2D rb; // reference for this object's Rigidbody2D
    private Vector2 movement;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;
    public bool obstacleDetected = false;
    public Transform targetPlayer = null;
    private bool canFire = true;
    private float firingTimer = 0f;
    private float spaceBetween = 1.5f;
    PhotonView view;

    void Awake() {
        patrollingObstacleDetection = GetComponentInChildren<PatrollingObstacleDetection>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // use rb to manipulate mvm and rotation of object
        rb = this.GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();

        bulletTransform = gameObject.transform.GetChild(0).transform;
    }

    public void takeDamage(int damage) {
        health -= damage;
        
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        // could instantiate an explosion animation here later
        view.RPC("disableObjectRPC", RpcTarget.AllBuffered, view.ViewID);
        // invoke to increase player score
        onEnemyDeath?.Invoke();
        // logic.addScore(1);
    }

    [PunRPC]
    private void disableObjectRPC(int viewID) {
        PhotonView.Find(viewID).gameObject.SetActive(false);
    }

    public void chasePlayer(Vector2 direction, Transform target) {
        if (Vector2.Distance(target.position, transform.position) >= spaceBetween) {
            // calculate the angle btwn enemy obj and player obj
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // set RigidBody's rotation
            rb.rotation = angle;

            rb.MovePosition((Vector2)transform.position + (direction * chaseSpeed * Time.deltaTime));
        }
        
    }

    public void patrol() {
        if (patrollingObstacleDetection.obstacleDetectedWhilePatrolling) {
            // turn around
            Vector3 newDir = RotationAngles.randomDir();
            transform.eulerAngles = newDir;
        } else {
            transform.position += transform.right * patrolSpeed * Time.deltaTime;
        }
    }

    public void shoot() {
        if (canFire) {
            canFire = false;
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletTransform.position, bulletTransform.rotation);
        } else {
            firingTimer += Time.deltaTime;
            if (firingTimer >= fireSpeed) {
                canFire = true;
                firingTimer = 0;
            }
        }
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
