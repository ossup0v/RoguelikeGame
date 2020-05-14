using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContainerV2 : MonoBehaviour
{
  public GameObject RoomToSpawn
  { get
    {
      if (IsStartRoom)
        return StartRoom;
      if (IsEndRoom && !IsAlreadySpawnedEndRoom)
      {
        IsAlreadySpawnedEndRoom = true;
        return EndRoom;
      }
      return RoomToSpawnObj;
    }
  }
  public GameObject RoomToSpawnObj;
  public GameObject BridgeToSpawn;
  public GameObject StartRoom;
  public GameObject EndRoom;
  public static bool IsStartRoom => SpawnedRooms.Count == 0;
  public static bool IsEndRoom => SpawnedRooms.Count >= MaxAmountOfRooms - 1;
  public static bool IsAlreadySpawnedEndRoom = false;
  public static List<GameObject> SpawnedRooms = new List<GameObject>();
  public static int MinAmountOfRooms = 10;
  public static int MaxAmountOfRooms = 10;
  public static bool ForceSpawn => SpawnedRooms.Count <= MinAmountOfRooms;
  public static bool IsCanSpawn => SpawnedRooms.Count <= MaxAmountOfRooms;
  public int CountOfWay = 10;
  public int StepOnSpawnInteration = 1;
}