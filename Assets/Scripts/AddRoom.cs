using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
  private RoomContainer container;

  private void Start()
  {
    container = GameObject.FindGameObjectWithTag("RoomContainer").GetComponent<RoomContainer>();
    container.Rooms.Add(this.gameObject);
  }
}