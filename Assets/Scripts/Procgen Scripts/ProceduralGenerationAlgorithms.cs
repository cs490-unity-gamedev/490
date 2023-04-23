using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

    //-----------------------------------------------------------------------
    // Adapted from "Procedural Dungeon Generation in Unity 2d" series
    // Author: Sunny Valley Studios
    //-----------------------------------------------------------------------

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> simpleRandomWalk(Vector2Int startPos, int walkLength) {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPos);

        Vector2Int prevPos = startPos;

        for (int i = 0; i < walkLength; i++) {
            Vector2Int curPos = prevPos + Direction2D.randomCardinalDir();
            path.Add(curPos);
            prevPos = curPos;
        }

        return path;
    }

    public static System.Tuple<List<Vector2Int>, Vector2Int> randomWalkCorridor(Vector2Int startPos, int corridorLength, Vector2Int lastDir) {
        List<Vector2Int> corridor = new List<Vector2Int>();

        Vector2Int dir;
        do {
            dir = Direction2D.randomCardinalDir();
        } while (dir == Direction2D.getOppositeDir(lastDir));
        lastDir = dir;

        Vector2Int curPos = startPos;
        corridor.Add(curPos);
        for (int i = 0; i < corridorLength; i++) {
            curPos += dir;
            corridor.Add(curPos);
        }

        return new System.Tuple<List<Vector2Int>, Vector2Int>(corridor, lastDir);
    }

    private delegate void SplitFunctionDelegate(int minHeight, int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room); // essentially the "function" data type
    public static List<BoundsInt> binarySpacePartioning(BoundsInt spaceToSplit, int minHeight, int minWidth) {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0) {
            BoundsInt room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth) {
                if (Random.value < 0.5f) {
                    if(room.size.y >= minHeight * 2) {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    } else if (room.size.x >= minWidth * 2) {
                        SplitVertically(minWidth, roomsQueue, room);
                    } else if (room.size.x >= minWidth && room.size.y >= minHeight) {
                        roomsList.Add(room);
                    }
                } else {
                    if (room.size.x >= minWidth * 2) {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2) {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) {
                        roomsList.Add(room);
                    }
                }
            }
        }
        
        return roomsList;
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)  {
        int ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room) {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }


}

public static class Direction2D {
    public static List<Vector2Int> cardinalDirs = new List<Vector2Int> {Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.down};
    public static Dictionary<Vector2Int, Vector2Int> oppositeDirs = new Dictionary<Vector2Int, Vector2Int> {
        {Vector2Int.up, Vector2Int.down},
        {Vector2Int.down, Vector2Int.up},
        {Vector2Int.right, Vector2Int.left},
        {Vector2Int.left, Vector2Int.right}
    };

    public static Vector2Int randomCardinalDir() {
        return cardinalDirs[UnityEngine.Random.Range(0, cardinalDirs.Count)];
    }

    public static Vector2Int getOppositeDir(Vector2Int dir) {
        if (dir == Vector2Int.zero) { return Vector2Int.zero; }
        return oppositeDirs[dir];
    }
}
