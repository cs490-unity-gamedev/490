using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehavior : MonoBehaviour
{
    //-----------------------------------------------------------------------
    // Adapted from "Context Steering AI" series
    // Author: Sunny Valley Studio
    //-----------------------------------------------------------------------

    public abstract (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, EnemyAIData enemyAIData);
}
