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

  private int countOfWay;

  private Func<int, int> ZeroOrMore => new Func<int, int>((o) => Math.Max(o, 0));
  private Func<int, int> OneOrMore => new Func<int, int>((o) => Math.Max(o, 1));

  void Start()
  {
    roomContainer = GameObject.FindGameObjectWithTag("RoomContainerV2").GetComponent<RoomContainerV2>();
    Invoke("Init", 0.05f);
    Invoke("Spawn", 0.1f);
    roomContainer.CountOfWay -= roomContainer.StepOnSpawnInteration;
  }

  private void Spawn()
  {
    if (isAlreadySpawned)
      return;

    restart:
    var allreadySpawnedRooms = RoomContainerV2.SpawnedRooms.Count;
    chanceToSpawnInProsent = ChanceToSpawnInProsent - allreadySpawnedRooms * StepInRandomChance;
    chanceToSpawnInProsent = RoomContainerV2.ForceSpawn && chanceToSpawnInProsent <= 10 ? 20 : chanceToSpawnInProsent;
    var maxcountOfWaysToSpawn = roomContainer.CountOfWay;
    availableCountOfWay = Mathf.Max(maxcountOfWaysToSpawn >= 4 ? 4 : maxcountOfWaysToSpawn,OneOrMore((chanceToSpawnInProsent - Random.Range(1, 5)) % 3));
    isWillSpawn = Random.Range(0, 100) <= chanceToSpawnInProsent;

    if (!isWillSpawn && RoomContainerV2.ForceSpawn)
      goto restart;

    if (!isWillSpawn || !RoomContainerV2.IsCanSpawn)
      return;

    List<int> availableWays = new List<int> { 0, 1, 2, 3 };
    for (int way = 0; way < availableCountOfWay; way++)
    {
      var w = availableWays[Random.Range(0, ZeroOrMore(availableWays.Count - 1))];
      var positionOfNewRoom = Positions[TypeOfElementPosition.Room];
      var posistion = positionOfNewRoom[w];

      Instantiate(roomToSpawn, posistion.Item1, Quaternion.identity);
      Instantiate(bridgeToSpawn, posistion.Item2, posistion.Item3);
      availableWays.Remove(w);
    }
    isAlreadySpawned = true;
  }

  //private void OnTriggerStay2D(Collider2D other)
  //{
  //  if (gameObject.CompareTag("Room") && other.CompareTag("Room"))
  //  {
  //    RoomContainerV2.SpawnedRooms.Remove(gameObject);
  //    Destroy(gameObject);
  //  }
  //}


  private void Init()
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

    roomToSpawn = roomContainer.RoomToSpawn;
    bridgeToSpawn = roomContainer.BridgeToSpawn;
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
