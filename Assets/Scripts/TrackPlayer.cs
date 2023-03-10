using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Transform player;
    public float cameraSmoothSpeed = 0.125f;
    private Vector3 locationOffset = new Vector3(0, 0, -10); // default camera position

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
