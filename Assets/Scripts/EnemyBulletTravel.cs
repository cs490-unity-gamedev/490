using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyBulletTravel : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    // when bullet hits player, damage player (1) and destroy bullet
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerController>().takeDamage(1);
        }
        gameObject.SetActive(false);
    }
}
