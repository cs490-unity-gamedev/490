using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletTravel : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
    }

    // when bullet hits enemy, deal 1 damage to enemy (health -= 1) and destroy bullet
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<EnemyController>().takeDamage(1);
        }  else if (collision.gameObject.tag == "Flock") {
            collision.gameObject.GetComponent<FlockUnit>().takeDamage(1);
        }
        gameObject.SetActive(false);
    }
}
