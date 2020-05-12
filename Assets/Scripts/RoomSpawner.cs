using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
  public int openingDirection;
  //1b
  //2t
  //3l
  //4r
  private static int indexer = 0;
  private RoomContainer _roomContainer;
  private bool isSpawned;

  private void Start()
  {
    _roomContainer = GameObject.FindGameObjectWithTag("RoomContainer").GetComponent<RoomContainer>();
    Invoke("Spawn", 0.1f);
    indexer++;
  }

  private void Spawn()
  {
    if (isSpawned)
      return;

    if (openingDirection == 1)
    {
      var rand = Random.Range(0, _roomContainer.BottomRoom.Length);
      var room = _roomContainer.BottomRoom[rand];
      Instantiate(room, transform.position, room.transform.rotation);
    }
    else if (openingDirection == 2)
    {
      var rand = Random.Range(0, _roomContainer.TopRoom.Length);
      var room = _roomContainer.TopRoom[rand];
      Instantiate(room, transform.position, room.transform.rotation);
    }
    else if (openingDirection == 3)
    {
      var rand = Random.Range(0, _roomContainer.LeftRoom.Length);
      var room = _roomContainer.LeftRoom[rand];
      Instantiate(room, transform.position, room.transform.rotation);
    }
    else if (openingDirection == 4)
    {
      var rand = Random.Range(0, _roomContainer.RightRoom.Length);
      var room = _roomContainer.RightRoom[rand];
      Instantiate(room, transform.position, room.transform.rotation);
    }
    isSpawned = true;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("SpawnPoint"))
    {
      if (isSpawned && other.GetComponent<RoomSpawner>().isSpawned == false && isSpawned == false)
      {
        Instantiate(_roomContainer.ClosedRoom, transform.position, Quaternion.identity);
        Debug.Log(indexer);
        Destroy(gameObject);
      }
      isSpawned = true;
    }
  }
}
