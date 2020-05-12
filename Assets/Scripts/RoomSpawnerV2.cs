using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnerV2 : MonoBehaviour
{
  public int ChanceToSpawnInProsent;
  ////we need get low random if spawned some rooms
  public int StepInRandomChance;
  private bool isWillSpawn;
  private GameObject roomToSpawn;
  private bool isAlreadySpawned;
  private int chanceToSpawnInProsent;
  private RoomContainerV2 roomContainer;
  //private int availableCountOfWay;
  //private Vector3[] PositionOfWays;
  // Start is called before the first frame update
  void Start()
  {
    //PositionOfWays = new Vector3[]
    //  {
    //    new Vector3(gameObject.transform.position.x - 15, gameObject.transform.position.y, gameObject.transform.position.z),
    //    new Vector3(gameObject.transform.position.x + 15, gameObject.transform.position.y, gameObject.transform.position.z),
    //    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 15, gameObject.transform.position.z),
    //    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 15, gameObject.transform.position.z),
    //  };
    roomContainer = GameObject.FindGameObjectWithTag("RoomContainerV2").GetComponent<RoomContainerV2>();

    var allreadySpawnedRooms = roomContainer.SpawnedRooms.Count;
    chanceToSpawnInProsent = ChanceToSpawnInProsent - allreadySpawnedRooms * StepInRandomChance;
    //var maxcountOfWaysToSpawn = roomContainer.CountOfWay;
    //availableCountOfWay = Mathf.Max(maxcountOfWaysToSpawn >= 4 ? 4 : maxcountOfWaysToSpawn, 1);

    isWillSpawn = Random.Range(0, 100) <= ChanceToSpawnInProsent;
    if (isWillSpawn/*(isWillSpawn || roomContainer.ForceSpawn) && roomContainer.IsCanSpawn)*/)
    {
      roomToSpawn = roomContainer.RoomToSpawn;
      Invoke("Spawn", 0.1f);
    }
    //roomContainer.CountOfWay -= roomContainer.StepOnSpawnInteration;
  }

  private void Spawn()
  {
    if (isAlreadySpawned)
      return;
    //List<int> availableWays = new List<int> { 0, 1, 2, 3 };
    //for (int way = 0; way <= availableCountOfWay; way++)
    //{
    //  var w = availableWays[Random.Range(0, availableWays.Count)];
    //  var posistion = PositionOfWays[w];
    //  availableWays.Remove(w);
      Instantiate(roomToSpawn, gameObject.transform.position, Quaternion.identity);
    //}
    isAlreadySpawned = true;
  }


  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("SpawnPointV2"))
    {
      if (other.GetComponent<RoomSpawnerV2>().isAlreadySpawned == false && isAlreadySpawned == false)
      {
        Destroy(gameObject);
      }
      isAlreadySpawned = true;
    }
  }
}
