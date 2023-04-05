using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

    public void generateDungeon() {
        tilemapVisualizer.clearTiles();
        runProceduralGeneration();
    }

    protected abstract void runProceduralGeneration();
}
