using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTravel : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    // when bullet hits enemy, destroy both bullet and enemy
    private void OnTriggerEnter2D(Collider2D collision) {
        print("collision");
        if (collision.gameObject.tag == "Player") {
            print("collision with player");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
