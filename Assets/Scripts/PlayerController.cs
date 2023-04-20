using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
<<<<<<< Updated upstream
using Mirror;
=======
using Photon.Pun;
>>>>>>> Stashed changes

public class PlayerController : NetworkBehaviour
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

    // MULTIPLAYER
    private PhotonView view;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        currHealth = maxHealth;
    }

    void Start()
    {
<<<<<<< Updated upstream
        if (!isLocalPlayer) {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
=======
        view = GetComponent<PhotonView>();
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if (!isLocalPlayer) { // if we are not the main client, do not run this method.
            return;
        }
        if (firingEnabled) {
            if (firing) {
                shoot();
=======
        if (view.IsMine) {
            if (firingEnabled) {
                if (firing) {
                    shoot();
                }
                tickFiringTimer(); // so fireSpeed can be enforced when player is not shooting
>>>>>>> Stashed changes
            }
            trackMouse();
        }
    }

    private void shoot() {
        if (canFire) {
            canFire = false;
            GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, bulletTransform.rotation);
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
<<<<<<< Updated upstream
        if (!isLocalPlayer) { // if we are not the main client, do not run this method.
            return;
        }
        move();
=======
        if (view.IsMine) {
            move();
        }
>>>>>>> Stashed changes
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
