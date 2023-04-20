using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    // META/STATE DATA
    public static event System.Action onPlayerDamaged;
    // public static event System.Action onPlayerDeath;
    public float maxHealth = 10;
    public float currHealth;

    // DEBUG/TESTING PARAMETERS
    public bool damageEnabled = true;
    public bool firingEnabled = true;

    // PARAMETERS AND CACHED VARIABLES
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float fireSpeed = 0.3f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletTransform;

    private Rigidbody2D rb;
    private Camera mainCam;

    // FIRING LOGIC
    private bool firing = false;
    private bool canFire = true;
    private float firingTimer = 0f;

    // INPUT VALUE READING
    private Vector2 moveInput;
    void OnMove(InputValue value) => moveInput = value.Get<Vector2>();
    private Vector2 lookInput;
    void OnLook(InputValue value) => lookInput = value.Get<Vector2>();
    private bool fireInput;
    void OnFire(InputValue value) => firing = !firing;

    // PHOTON
    PhotonView view;

    void Awake()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine) {
            rb = GetComponent<Rigidbody2D>();
            currHealth = maxHealth;
        }
    }

    void Start()
    {
        if (view.IsMine) {
            mainCam = transform.Find("Camera").GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine) {
            if (firingEnabled) {
                if (firing) {
                    shoot();
                }
                tickFiringTimer(); // so fireSpeed can be enforced when player is not shooting
            }
            trackMouse();
        }
    }

    private void shoot() {
        if (canFire) {
            canFire = false;
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletTransform.position, bulletTransform.rotation);
        }
    }

    private void tickFiringTimer() {
        if (!canFire) {
            firingTimer += Time.deltaTime;
            if (firingTimer >= fireSpeed) {
                canFire = true;
                firingTimer = 0;
            }
        }
    }

    void trackMouse() {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(lookInput);

        Vector3 rotation = mousePos - transform.position;
        float rotZVal = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZVal);
    }

    void FixedUpdate() // used for movement/interactions with physics engine
    {
        if (view.IsMine) {
            move();
        }
    }

    private void move() {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    public void takeDamage(int damage) {
        if (damageEnabled) {
            currHealth -= damage;
            onPlayerDamaged?.Invoke();
            print("health: " + currHealth);
            
            if (currHealth <= 0) {
                Die();
            }
        }
    }

    private void Die() {
        // could instantiate an explosion animation here later
        Destroy(gameObject);
        // load game over screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
