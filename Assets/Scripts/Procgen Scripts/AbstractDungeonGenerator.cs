using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //-----------------------------------------------------------------------
    // Adapted from "Procedural Dungeon Generation in Unity 2d" series
    // Author: Sunny Valley Studios
    //-----------------------------------------------------------------------

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

    public void generateDungeon() {
        tilemapVisualizer.clearTiles();
        runProceduralGeneration();
        instantiateEnemies();
    }

    protected abstract void runProceduralGeneration();
    protected abstract void instantiateEnemies();
}
