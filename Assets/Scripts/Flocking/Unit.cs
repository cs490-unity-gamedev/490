using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GameObject manager;
    private Vector2 location = Vector2.zero;
    private Vector2 velocity;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce; // force being placed on current particle, pushing it around

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        manager = FindObjectOfType<AllUnits>().gameObject;
    }

    // gets vector towards target location
    Vector2 seek(Vector2 target) {
        return (target - location);
    }

    void applyForce(Vector2 force2D) {
        Vector3 force3D = new Vector3(force2D.x, force2D.y, 0); // turn 2D force into 3D force
        this.GetComponent<Rigidbody2D>().AddForce(force3D);
    }

    void flock() {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        Vector2 goalLocation = seek(goalPos);
        currentForce = goalLocation.normalized;

        applyForce(currentForce);
    }

    // Update is called once per frame
    void Update()
    {
        flock();
        goalPos = manager.transform.position;
    }
}
