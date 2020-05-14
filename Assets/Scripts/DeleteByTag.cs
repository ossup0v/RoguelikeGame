using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteByTag : MonoBehaviour
{
  public string CurrentTag = "Room";
  public string OtherTag = "Room";

  private MarkAsSpawned spawned;
  private void Start()
  {
    spawned = gameObject.GetComponentInChildren<MarkAsSpawned>();
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (gameObject.CompareTag(CurrentTag) && other.CompareTag(OtherTag))
    {
      var otherIndex = other.GetComponent<MarkAsSpawned>().Index;
      var currentIndex = spawned.Index;
      if (otherIndex > currentIndex)
      {
        RoomContainerV2.SpawnedRooms.Remove(other.gameObject);
        Destroy(other.gameObject);
      }
      else
      {
        RoomContainerV2.SpawnedRooms.Remove(gameObject);
        Destroy(gameObject);
      }
    }
  }
}
