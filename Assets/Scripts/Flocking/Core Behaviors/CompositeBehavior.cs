using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    //-----------------------------------------------------------------------
    // Adapted from "Flocking Algorithm in Unity" series
    // Author: Board To Bits Games, 2019
    //-----------------------------------------------------------------------

    [SerializeField] FlockBehavior[] behaviors;
    [SerializeField] float[] weights;

    public override Vector2 calculateMove(FlockUnit unit, List<Transform> context, Flock flock) {
        // handle data mismatch
        if (weights.Length != behaviors.Length) {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        // set up move
        Vector2 move = Vector2.zero;

        // iterate through behaviors
        for (int i = 0; i < behaviors.Length; i++) {
            Vector2 partialMove = behaviors[i].calculateMove(unit, context, flock) * weights[i];

            if (partialMove != Vector2.zero) {
                // check if partial move exceeds the weight
                if (partialMove.sqrMagnitude > weights[i] * weights[i]) {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move += partialMove;
            }
        }

        return move;
    }
}
