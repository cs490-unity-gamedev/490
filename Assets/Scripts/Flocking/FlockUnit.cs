using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FlockUnit : MonoBehaviour
{
    //-----------------------------------------------------------------------
    // Adapted from "Flocking Algorithm in Unity" series
    // Author: Board To Bits Games, 2019
    //-----------------------------------------------------------------------

    public Collider2D unitCollider;

    // EnemyController
    public static event System.Action onEnemyDeath;
    [SerializeField] private int health = 1;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        unitCollider = GetComponent<Collider2D>();
        view = GetComponent<PhotonView>();
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerController>().takeDamage(1);
        }
        Die();
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
}
