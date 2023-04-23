using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //-----------------------------------------------------------------------
    // Adapted from "Procedural Dungeon Generation in Unity 2d" series
    // Author: Sunny Valley Studios
    //-----------------------------------------------------------------------

public static class WallGenerator
{
    public static void createWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer) {
        HashSet<Vector2Int> basicWallPositions = findWallsInDirections(floorPositions, Direction2D.cardinalDirs);

        foreach (Vector2Int pos in basicWallPositions) {
            tilemapVisualizer.paintSingleBasicWall(pos);
        }
    }

    private static HashSet<Vector2Int> findWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> dirs)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (Vector2Int pos in floorPositions) {
            foreach (Vector2Int direction in dirs) {
                Vector2Int neighborPos = pos + direction;
                if (!floorPositions.Contains(neighborPos)) {
                    wallPositions.Add(neighborPos);
                }
            }
        }

        return wallPositions;
    }
}
