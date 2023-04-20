using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField] [Range(0.1f, 1f)] private float roomPercent = 0.8f; // exact minimum of 2 rooms to avoid dead ends

    protected override void runProceduralGeneration() {
        tilemapVisualizer.clearTiles();
        corridorFirstGenerate();
<<<<<<< Updated upstream
=======
        instantiateEnemies();
    }

    protected override void instantiateEnemies() {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(0);
            for (int j = 0; j < child.childCount; j++) {
                DestroyImmediate(child.GetChild(0).gameObject);
            }
            DestroyImmediate(child.gameObject); // assume all children are enemy parent objects. clear all enemies.
        }
        GameObject newEnemiesChild = new GameObject("Enemies");
        newEnemiesChild.transform.parent = transform;

        foreach (HashSet<Vector2Int> room in rooms) {
            List<Vector2Int> roomPositions = room.ToList();

            for (int i = 0; i < enemiesPerRoom; i++) {
                Vector2Int posInRoom = roomPositions[UnityEngine.Random.Range(0, roomPositions.Count)];
                Vector3Int tilePos = tilemapVisualizer.floorTilemap.WorldToCell((Vector3Int) posInRoom);
                PhotonNetwork.Instantiate(enemyPrefab.name, tilePos, transform.rotation);
            }
        }
>>>>>>> Stashed changes
    }

    private void corridorFirstGenerate() {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        if (PhotonNetwork.IsMasterClient) {
            List<Vector2Int> potentialRoomPositions = new List<Vector2Int>();

            createCorridors(floorPositions, potentialRoomPositions);
            HashSet<Vector2Int> roomPositions = createRooms(potentialRoomPositions);
            floorPositions.UnionWith(roomPositions);

            // PHOTON conversion below
            int[] photonXTiles = new int[floorPositions.Count];
            int[] photonYTiles = new int[floorPositions.Count];
            int numTile = 0;
            foreach (Vector2Int tile in floorPositions) {
                photonXTiles[numTile] = tile.x;
                photonYTiles[numTile] = tile.y;
                numTile++;
            }

            Hashtable photonHash = new Hashtable();
            photonHash.Add("TilesX", photonXTiles);
            photonHash.Add("TilesY", photonYTiles);
            PhotonNetwork.CurrentRoom.SetCustomProperties(photonHash);
        } else {
            int[] photonXTiles = (int[]) PhotonNetwork.CurrentRoom.CustomProperties["TilesX"];
            int[] photonYTiles = (int[]) PhotonNetwork.CurrentRoom.CustomProperties["TilesY"];
            for (int numTile = 0; numTile < photonXTiles.Length; numTile++) {
                Vector2Int tile = new Vector2Int(photonXTiles[numTile], photonYTiles[numTile]);
                floorPositions.Add(tile);
            }
        }

        tilemapVisualizer.paintFloorTiles(floorPositions);
        WallGenerator.createWalls(floorPositions, tilemapVisualizer);
    }

    private void createCorridors(HashSet<Vector2Int> floorPositions, List<Vector2Int> potentialRoomPositions) {
        Vector2Int curPos = startPos;
        potentialRoomPositions.Add(curPos);

        Vector2Int lastDir = Vector2Int.zero;
        for (int i = 0; i < corridorCount; i++) {
            Tuple<List<Vector2Int>, Vector2Int> corridorAndLastDir = ProceduralGenerationAlgorithms.randomWalkCorridor(curPos, corridorLength, lastDir);
            List<Vector2Int> corridor = corridorAndLastDir.Item1;
            lastDir = corridorAndLastDir.Item2;
            curPos = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(curPos);
            floorPositions.UnionWith(corridor);
        }
    }

    private HashSet<Vector2Int> createRooms(List<Vector2Int> potentialRoomPositions) {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        int roomCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent) - 2;

        List<Vector2Int> roomsToCreate = new List<Vector2Int>();
        roomsToCreate.Add(potentialRoomPositions[0]); // first in path may be a dead end
        roomsToCreate.Add(potentialRoomPositions[potentialRoomPositions.Count - 1]); // last in path may be a dead end
        potentialRoomPositions.RemoveAt(0); // first will already be a room, make sure it doesn't get added again
        potentialRoomPositions.RemoveAt(potentialRoomPositions.Count - 1); // last will already be a room, make sure it doesn't get added again
        
        if (roomCount > 0) {
            List<Vector2Int> randomNonDeadEndRoomPositions = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList(); //TODO: performance pain point due to 'random sort' by guid, can implement Fisher-Yates
            roomsToCreate = roomsToCreate.Union(randomNonDeadEndRoomPositions).ToList();
        }

        foreach (Vector2Int roomPos in roomsToCreate) {
            HashSet<Vector2Int> roomFloor = runRandomWalk(roomPos, randomWalkParameters);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }
}
