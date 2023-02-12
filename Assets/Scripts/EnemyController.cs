using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // public Transform player;
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
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the direction our player is compared to our enemy obj
        Vector3 direction = player.transform.position - transform.position;
        // calculate the angle btwn enemy obj and player obj
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // set RigidBody's rotation
        rb.rotation = angle;
        direction.Normalize(); // bc we want movement val to be btwn -1 and 1
        movement = direction;
    }

    private void FixedUpdate() {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
