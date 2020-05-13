using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContainerV2 : MonoBehaviour
{
  public GameObject RoomToSpawn;
  public GameObject BridgeToSpawn;
  public GameObject StartRoom;
  public GameObject EndRoom;
  public bool IsStartRoom => SpawnedRooms.Count == 0;
  public bool IsEndRoom => SpawnedRooms.Count == MaxAmountOfRooms - 1;
  public List<GameObject> SpawnedRooms;
  public int MinAmountOfRooms = 20;
  public int MaxAmountOfRooms = 100;
  public bool ForceSpawn => SpawnedRooms.Count <= MinAmountOfRooms;
  public bool IsCanSpawn => SpawnedRooms.Count <= MaxAmountOfRooms;
  public int CountOfWay = 10;
  public int StepOnSpawnInteration = 1;
}