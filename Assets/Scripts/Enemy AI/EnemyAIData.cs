using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIData : MonoBehaviour
{
    //-----------------------------------------------------------------------
    // Adapted from "Context Steering AI" series
    // Author: Sunny Valley Studio, 2022
    //-----------------------------------------------------------------------

    public List<Transform> targets = null;
    public Collider2D[] obstacles = null;

    public Transform currentTargetPlayer;

    public int GetTargetsCount() => targets == null ? 0 : targets.Count;
}
