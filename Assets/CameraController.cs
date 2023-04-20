using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    private PhotonView view;
    private GameObject cameraObject;

    void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine) {
            cameraObject = transform.Find("Camera").gameObject;
            cameraObject.SetActive(true);
        }
    }

    void LateUpdate()
    {
        if (view.IsMine) {
            cameraObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
