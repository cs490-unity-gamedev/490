using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

    //-----------------------------------------------------------------------
    // Adapted from "Procedural Dungeon Generation in Unity 2d" series
    // Author: Sunny Valley Studios
    //-----------------------------------------------------------------------

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] public Tilemap floorTilemap, wallTilemap;
    [SerializeField] private TileBase floorTile, wallTop; // could be made into an array and select randomly

    public void paintFloorTiles(IEnumerable<Vector2Int> floorPositions) {
        paintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void paintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile) {
        foreach (Vector2Int pos in positions) {
            paintTileAtPos(pos, tilemap, tile);
        }
    }

    private void paintTileAtPos(Vector2Int position, Tilemap tilemap, TileBase tile) {
        Vector3Int tilePos = tilemap.WorldToCell((Vector3Int) position);
        tilemap.SetTile(tilePos, tile);
    }

    public void clearTiles() {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void paintSingleBasicWall(Vector2Int pos)
    {
        paintTileAtPos(pos, wallTilemap, wallTop);
    }
}
