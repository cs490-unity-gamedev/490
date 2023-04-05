using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Transform player;
    public float cameraSmoothSpeed = 0.125f;
    private Vector3 locationOffset = new Vector3(0, 0, -10); // default camera position

    void Start() {
        player = gameObject.transform.parent.transform;
        gameObject.transform.SetParent(null, true);
    }

    void LateUpdate()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void FixedUpdate()
    {
        if (player != null) {
            Vector3 desiredPosition = player.position + player.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSmoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
