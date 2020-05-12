using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoomV2 : MonoBehaviour
{
  private RoomContainerV2 container;

  private void Start()
  {
    container = GameObject.FindGameObjectWithTag("RoomContainerV2").GetComponent<RoomContainerV2>();
    container.SpawnedRooms.Add(this.gameObject);
  }
}
