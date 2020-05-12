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
    var type = ConvertToRoomDirectionType(openingDirection);

    if (type != RoomDirectionType.Unexpected)
    {
      var rooms = _roomContainer.AvailableRooms[type];
      var rand = Random.Range(0, rooms.Length);
      Instantiate(rooms[rand], transform.position, rooms[rand].transform.rotation);
    }
    isSpawned = true;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("SpawnPoint"))
    {
      if (other.GetComponent<RoomSpawner>().isSpawned == false && isSpawned == false)
      {
        Instantiate(_roomContainer.ClosedRoom, transform.position, Quaternion.identity);
        Debug.Log(indexer);
        Destroy(gameObject);
      }
      isSpawned = true;
    }
  }

  private RoomDirectionType ConvertToRoomDirectionType(int i)
  {
    if (i == 1)
    { return RoomDirectionType.Bottom; }
    else if (i == 2)
    { return RoomDirectionType.Top; }
    else if (i == 3)
    { return RoomDirectionType.Left; }
    else if (i == 4)
    { return RoomDirectionType.Right; }

    return RoomDirectionType.Unexpected;
  }
}
