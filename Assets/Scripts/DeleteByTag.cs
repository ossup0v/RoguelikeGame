using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteByTag : MonoBehaviour
{
  public string CurrentTag = "Room";
  public string OtherTag = "Room";

  private RoomContainerV2 roomContainer;
  private MarkAsSpawned spawned;
  private void Start()
  {
    spawned = gameObject.GetComponentInChildren<MarkAsSpawned>();
    roomContainer = GameObject.FindGameObjectWithTag("RoomContainerV2").GetComponent<RoomContainerV2>();
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (gameObject.CompareTag("Room") && other.CompareTag("Room") /*&& (
      (!spawned.IsSpawned && other.GetComponent<MarkAsSpawned>().IsSpawned)
      || (spawned.IsSpawned && !other.GetComponent<MarkAsSpawned>().IsSpawned))*/)
    {
      var toDelete = other.GetComponent<MarkAsSpawned>().Index > spawned.Index ? other.gameObject : gameObject;
      if (other.GetComponent<MarkAsSpawned>().Index > spawned.Index)
      {
        roomContainer.SpawnedRooms.Remove(other.gameObject);
        Destroy(other.gameObject);
      }
      else
      {
        roomContainer.SpawnedRooms.Remove(gameObject);
        Destroy(gameObject);
      }
    }
  }
}
