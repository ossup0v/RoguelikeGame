using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoomV2 : MonoBehaviour
{
  private RoomContainerV2 container;

  private void Start()
  {
    RoomContainerV2.SpawnedRooms.Add(this.gameObject);
    Debug.Log("rooms already spawned: " + RoomContainerV2.SpawnedRooms.Count);
  }
}
