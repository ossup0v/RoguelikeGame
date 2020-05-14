using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnManager : MonoBehaviour
{
  public static int CountOfRooms = 10;
  public new GameObject RoomToSpawn
  {
    get
    {
      var roomToSpawn = default(GameObject);
      if (countOfSpawnedRooms == 0)
        roomToSpawn = StartRoom;
      else if (countOfSpawnedRooms == 4)
        roomToSpawn = HealthRoom;
      else if(countOfSpawnedRooms == 5)
        roomToSpawn = ShopRoom;
      else if(countOfSpawnedRooms == 8)
        roomToSpawn = BossRoom;
      else
        roomToSpawn = DefaultRoom;
      countOfSpawnedRooms++;
      return roomToSpawn;
    }
  }
  public GameObject DefaultRoom;
  public GameObject StartRoom;
  public GameObject ShopRoom;
  public GameObject HealthRoom;
  public GameObject BossRoom;


  public GameObject BridgeToSpawn
  {
    get
    {
      return BridgeToSpawnObj;
    }
  }
  public GameObject BridgeToSpawnObj;
  public static bool ForceSpawn => RoomSpawned.Count <= CountOfRooms;
  public static bool CanSpawn => RoomSpawned.Count <= CountOfRooms;
  public static List<GameObject> RoomSpawned = new List<GameObject>();
  public static List<Vector3> CapturedPlaces = new List<Vector3>();
  private static bool IsShopRoomSpawned = false;
  private static bool IsHealthRoomSpawned = false;
  private static bool IsBossRoomSpawned = false;
  private static int countOfSpawnedRooms = 1;
}
