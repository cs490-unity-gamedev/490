using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// populate flock w/ prefabs + handle iteration and execute behaviors on flock units
public class Flock : MonoBehaviour
{
    //-----------------------------------------------------------------------
    // Adapted from "Flocking Algorithm in Unity" series
    // Author: Board To Bits Games, 2019
    //-----------------------------------------------------------------------

    [SerializeField] FlockUnit unitPrefab;
    private List<FlockUnit> units = new List<FlockUnit>();
    [SerializeField] FlockBehavior behavior;

    // number of units to create in the scene
    [Range(10, 500)]
    [SerializeField] int startingCount = 50;
    private const float UnitDensity = 0.8f; // size of circle (where units spawn) based on number of units inside the flock

    [Range(1f, 100f)]
    [SerializeField] float driveFactor = 10f;
    [Range(1f, 100f)]
    [SerializeField] float maxSpeed = 5f;
    [Range(1f, 10f)]
    [SerializeField] float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    [SerializeField] float avoidanceRadiusMultiplier = 0.5f;

    private float squareMaxSpeed;
    private float squareNeighborRadius;
    public float squareAvoidanceRadius;

    // Start is called before the first frame update
    void Start()
    {
        // compare the squares instead of getting square root every time
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        // initialize and instantiate the flock
        for (int i = 0; i < startingCount; i++) {
            GameObject newUnit = PhotonNetwork.InstantiateRoomObject(
                unitPrefab.name,
                Random.insideUnitCircle * startingCount * UnitDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f))
                );
            newUnit.name = "Unit " + i;
            // newUnit.Initialize(this);
            units.Add(newUnit.GetComponent<FlockUnit>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // iterate through units
        foreach (FlockUnit unit in units) {
            // what things exist in the context of neighbor radius
            List<Transform> context = getNearbyObjects(unit);

            Vector2 move = behavior.calculateMove(unit, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed) {
                move = move.normalized * maxSpeed; // change back to max speed
            }
            unit.Move(move);
        }
    }

    List<Transform> getNearbyObjects(FlockUnit unit) {
        // using Unity's physics system
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(unit.transform.position, neighborRadius);
        foreach (Collider2D collider in contextColliders) {
            if (collider != unit.unitCollider) {
                context.Add(collider.transform);
            }
        }
        return context;
    }
}
