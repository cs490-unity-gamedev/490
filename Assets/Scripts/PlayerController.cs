using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lookSpeed = 1f;
    public GameObject bulletPrefab;
    public Transform bulletTransform;

    private Rigidbody2D rb;
    private Camera mainCam;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool fireInput;
    private bool firing = false;
    private bool canFire = true;
    public float fireSpeed = 0.3f;
    private float fireTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firing) {
            shoot();
        }
        // has to be separate from firing, so that the timer can increase when out of combat to allow player to click multiple times to fire
        if (!canFire) {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireSpeed) {
                canFire = true;
                fireTimer = 0;
            }
        }
    }

    void FixedUpdate() {
        // use for movement
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        trackMouse();
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value) {
        lookInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value) {
        firing = !firing;
    }

    void shoot() {
        if (canFire) {
            canFire = false;
            GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, bulletTransform.rotation);
        }
    }

    void trackMouse() {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(lookInput);

        Vector3 rotation = mousePos - transform.position;
        float rotZVal = (Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg) + 90;

        transform.rotation = Quaternion.Euler(0, 0, rotZVal);
    }
}
