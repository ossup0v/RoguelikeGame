using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawnerV2 : MonoBehaviour
{
  public int ChanceToSpawnInProsent;
  ////we need get low random if spawned some rooms
  public int StepInRandomChance;
  private bool isWillSpawn;
  private GameObject roomToSpawn;
  private GameObject roomToSpawnOnStart;
  private GameObject roomToSpawnOnEnd;
  private GameObject bridgeToSpawn;
  private bool isAlreadySpawned;
  private int chanceToSpawnInProsent;
  private RoomContainerV2 roomContainer;
  private int availableCountOfWay;
  private Vector3[] PositionOfNewRoom;
  private Vector3[] PositionOfBridge;
  private Dictionary<TypeOfElementPosition
    , Tuple<
      //point of room
      Vector3
    //point of bridge
    , Vector3
    //Quaternion of bridge
    , Quaternion>[]> Positions = new Dictionary<TypeOfElementPosition, Tuple<Vector3, Vector3, Quaternion>[]>();
  private static float StepBetweenRooms = 15f;
  private float StepBridge = StepBetweenRooms / 2.0f;
  private bool isEndRoom;
  private bool isStartRoom;
  public bool IsStartRoom;
  // Start is called before the first frame update
  void Start()
  {
    Positions[TypeOfElementPosition.Room] = new Tuple<Vector3, Vector3, Quaternion>[]
      {
        new Tuple<Vector3, Vector3, Quaternion>(
            new Vector3(gameObject.transform.position.x - StepBetweenRooms, gameObject.transform.position.y, gameObject.transform.position.z)
          , new Vector3(gameObject.transform.position.x - StepBridge, gameObject.transform.position.y, gameObject.transform.position.z + 1f)
          , Quaternion.identity),
        new Tuple<Vector3, Vector3, Quaternion>(
            new Vector3(gameObject.transform.position.x + StepBetweenRooms, gameObject.transform.position.y, gameObject.transform.position.z)
          , new Vector3(gameObject.transform.position.x + StepBridge, gameObject.transform.position.y, gameObject.transform.position.z + 1f)
          ,  Quaternion.identity),
        new Tuple<Vector3, Vector3, Quaternion>(
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - StepBetweenRooms, gameObject.transform.position.z)
          , new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - StepBridge, gameObject.transform.position.z + 1f)
          , new Quaternion(90f,90f,0,0)),
        new Tuple<Vector3, Vector3, Quaternion>(
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + StepBetweenRooms, gameObject.transform.position.z)
          , new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + StepBridge, gameObject.transform.position.z + 1f)
          , new Quaternion(90f,90f,0,0))
      };

    roomContainer = GameObject.FindGameObjectWithTag("RoomContainerV2").GetComponent<RoomContainerV2>();

    var allreadySpawnedRooms = roomContainer.SpawnedRooms.Count;
    chanceToSpawnInProsent = ChanceToSpawnInProsent - allreadySpawnedRooms * StepInRandomChance;
    var maxcountOfWaysToSpawn = roomContainer.CountOfWay;
    availableCountOfWay = Mathf.Max(maxcountOfWaysToSpawn >= 4 ? 4 : maxcountOfWaysToSpawn, 1);

    isWillSpawn = Random.Range(0, 100) <= chanceToSpawnInProsent;
    if (roomContainer.IsStartRoom)
    {
      roomToSpawnOnStart = roomContainer.StartRoom;
      isStartRoom = true;
      IsStartRoom = true;
    }

    if (roomContainer.IsEndRoom)
    {
      roomToSpawnOnEnd = roomContainer.EndRoom;
      isEndRoom = true;
    }
    if (isWillSpawn/*(isWillSpawn || roomContainer.ForceSpawn)*/ && roomContainer.IsCanSpawn)
    {
      roomToSpawn = roomContainer.RoomToSpawn;
      bridgeToSpawn = roomContainer.BridgeToSpawn;
      Invoke("Spawn", 0.1f);
    }
    roomContainer.CountOfWay -= roomContainer.StepOnSpawnInteration;
  }

  private void Spawn()
  {
    if (isAlreadySpawned)
      return;
    List<int> availableWays = new List<int> { 0, 1, 2, 3 };
    for (int way = 0; way <= availableCountOfWay; way++)
    {
      var w = availableWays[Random.Range(0, availableWays.Count)];
      var positionOfNewRoom = Positions[TypeOfElementPosition.Room];
      var posistion = positionOfNewRoom[w];
      if (isEndRoom)
      {
        Instantiate(roomToSpawnOnEnd, posistion.Item1, Quaternion.identity);
        isEndRoom = false;
      }
      else if (isStartRoom)
      {
        Instantiate(roomToSpawnOnStart, posistion.Item1, Quaternion.identity);
        isStartRoom = false;
      }
      else
      {
        Instantiate(roomToSpawn, posistion.Item1, Quaternion.identity);
      }
      Instantiate(bridgeToSpawn, posistion.Item2/*gameObject.transform.position*/, posistion.Item3/*Quaternion.identity*/);
      availableWays.Remove(w);
    }
    isAlreadySpawned = true;
  }

  //private void OnCollisionEnter2D(Collision2D other)
  //{
  //  if (gameObject.CompareTag("Room") && other.gameObject.CompareTag("Room"))
  //  {
  //    roomContainer.SpawnedRooms.Remove(other.gameObject);
  //    Destroy(other.gameObject);
  //  }
  //}

  private void OnTriggerStay2D(Collider2D other)
  {
    if (gameObject.CompareTag("Room") && other.CompareTag("Room"))
    {
      roomContainer.SpawnedRooms.Remove(gameObject);
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    //if (gameObject.CompareTag("Room") && other.CompareTag("Room"))
    //{
    //  roomContainer.SpawnedRooms.Remove(gameObject);
    //  Destroy(gameObject);
    //}
    //if (other.CompareTag("SpawnPointV2"))
    //{
    //  if (other.GetComponent<RoomSpawnerV2>().isAlreadySpawned == false && isAlreadySpawned == false /*&& !IsStartRoom*/)
    //  {
    //    roomContainer.SpawnedRooms.Remove(gameObject);
    //    Destroy(gameObject);
    //  }
    //  isAlreadySpawned = true;
    //}
  }

  public bool IsAlreadySpawned()
  {
    return isAlreadySpawned;
  }

  private enum TypeOfElementPosition
  {
    Room,
    Bridge,
  }
}
