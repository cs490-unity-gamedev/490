using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject procgenPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        GameObject generator = PhotonNetwork.Instantiate(procgenPrefab.name, Vector3.zero, Quaternion.identity);
        generator.transform.GetComponentInChildren<CorridorFirstDungeonGenerator>().generateDungeon();
    }
}
