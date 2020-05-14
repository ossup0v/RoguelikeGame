using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{
  public int PercentToSpawn = 50;
  public int StepPrecentToSpawn = 5;

  private GameObject roomToSpawn;
  private GameObject bridgeToSpawn;
  private RoomSpawnManager roomSpawnManager;

  private const float stepBetweenRooms = 15f;
  private const float bridgeStep = stepBetweenRooms / 2f;

  private List<Tuple<Vector3, Vector3, Quaternion>> placesToSpawn = new List<Tuple<Vector3, Vector3, Quaternion>>();
  private Func<int, int> ZeroOrMore = new Func<int, int>(v => Math.Max(v, 0));

  void Start()
  {
    CheckAndCapturePlace(gameObject.transform.position);
    roomSpawnManager = GameObject.FindGameObjectWithTag("RoomSpawnManager").GetComponent<RoomSpawnManager>();
    bridgeToSpawn = roomSpawnManager.BridgeToSpawn;
    Invoke("Init", 0.05f);
    Invoke("Spawn", 0.1f/*Random.Range(0.1f, 0.25f)*/);
  }

  private void Spawn()
  {
    if (!RoomSpawnManager.CanSpawn)
      return;
    var availablePlaces = new List<int> { 0, 1, 2, 3 };

    restart:
    if (PercentToSpawn < 20 && RoomSpawnManager.ForceSpawn)
      PercentToSpawn = 20;
    var percentToSpawn = ZeroOrMore(PercentToSpawn - (RoomSpawnManager.RoomSpawned.Count * StepPrecentToSpawn));
    var isWillSpawn = Random.Range(0, 100) <= PercentToSpawn;
    var countOfPoints = Random.Range(1, 4);

    if (!isWillSpawn && RoomSpawnManager.ForceSpawn)
      goto restart;

    for (int i = 1; i <= countOfPoints && availablePlaces.Count > 0;)
    {
      var indexOfIndex = Random.Range(0, availablePlaces.Count);
      var indexPlace = availablePlaces[indexOfIndex];
      availablePlaces.Remove(indexPlace);
      var placePoint = placesToSpawn[indexPlace];
      if (CheckAndCapturePlace(placePoint.Item1))
      {
        var room = roomSpawnManager.RoomToSpawn;
        Instantiate(room, placePoint.Item1, Quaternion.identity);
        Instantiate(bridgeToSpawn, placePoint.Item2, placePoint.Item3);
        RoomSpawnManager.RoomSpawned.Add(room);
        i++;
      }
      else
      { /*nope?*/ }

      if (i == 1 && availablePlaces.Count == 0)
        goto restart;
    }
  }

  private void Init()
  {
    placesToSpawn = new List<Tuple<Vector3, Vector3, Quaternion>>
     {
        new Tuple<Vector3, Vector3, Quaternion>(
            new Vector3(gameObject.transform.position.x - stepBetweenRooms, gameObject.transform.position.y, gameObject.transform.position.z)
          , new Vector3(gameObject.transform.position.x - bridgeStep, gameObject.transform.position.y, gameObject.transform.position.z - 1f)
          , Quaternion.identity),
        new Tuple<Vector3, Vector3, Quaternion>(
            new Vector3(gameObject.transform.position.x + stepBetweenRooms, gameObject.transform.position.y, gameObject.transform.position.z)
          , new Vector3(gameObject.transform.position.x + bridgeStep, gameObject.transform.position.y, gameObject.transform.position.z - 1f)
          ,  Quaternion.identity),
        new Tuple<Vector3, Vector3, Quaternion>(
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - stepBetweenRooms, gameObject.transform.position.z)
          , new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - bridgeStep, gameObject.transform.position.z - 1f)
          , new Quaternion(90f,90f,0,0)),
        new Tuple<Vector3, Vector3, Quaternion>(
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + stepBetweenRooms, gameObject.transform.position.z)
          , new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + bridgeStep, gameObject.transform.position.z - 1f)
          , new Quaternion(90f,90f,0,0))
     };

  }
  private bool CheckAndCapturePlace(Vector3 place)
  {
    if (RoomSpawnManager.CapturedPlaces.Contains(place))
      return false;
    else
    {
      RoomSpawnManager.CapturedPlaces.Add(place);
      return true;
    }
  }
}
