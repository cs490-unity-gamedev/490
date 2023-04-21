using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnits : MonoBehaviour
{
    private GameObject[] units;
    [SerializeField] GameObject unitPrefab;
    [SerializeField] int numUnits = 10;
    [SerializeField] Vector3 range = new Vector3(5,5,5);

    // Start is called before the first frame update
    void Start()
    {
        units = new GameObject[numUnits];
        for (int i = 0; i < numUnits; i++) {
            Vector3 unitPos = new Vector3(
                Random.Range(-range.x, range.x),
                Random.Range(-range.y, range.y),
                Random.Range(0,0));
            units[i] = Instantiate(unitPrefab, this.transform.position + unitPos, Quaternion.identity) as GameObject;
        }
    }
}
