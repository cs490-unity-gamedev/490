using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected SimpleRandomWalkScriptableObject randomWalkParameters;

    protected override void runProceduralGeneration() {
        HashSet<Vector2Int> floorPositions = runRandomWalk(startPos, randomWalkParameters);
        tilemapVisualizer.paintFloorTiles(floorPositions);
        WallGenerator.createWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> runRandomWalk(Vector2Int pos, SimpleRandomWalkScriptableObject parameters) {
        Vector2Int curPos = pos;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < parameters.iterations; i++) {
            HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.simpleRandomWalk(curPos, parameters.walkLength);
            floorPositions.UnionWith(path);
            if (parameters.startRandomOnEachIteration) {
                curPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        
        return floorPositions;
    }

    protected override void instantiateEnemies() {
        throw new NotImplementedException();
    }
}
