using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detector : MonoBehaviour
{
    //-----------------------------------------------------------------------
    // Adapted from "Context Steering AI" series
    // Author: Sunny Valley Studio, 2022
    //-----------------------------------------------------------------------

    public abstract void Detect(EnemyAIData enemyAIData);
}
