using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContainer : MonoBehaviour
{
  public GameObject[] LeftRoom;
  public GameObject[] RightRoom;
  public GameObject[] TopRoom;
  public GameObject[] BottomRoom;

  public GameObject ClosedRoom;

  public List<GameObject> Rooms;

  public float WaitTime;
  private bool IsBossSpawned = false;
  public GameObject Boss;

  private void Update()
  {
    if (WaitTime <= 0 && !IsBossSpawned)
    {
      for (int i = 0; i < Rooms.Count; i++)
        if (i == Rooms.Count - 1)
        {
          Instantiate(Boss, Rooms[i].transform.position, Quaternion.identity);
          IsBossSpawned = true;
        }
    }
    else
    {
      WaitTime -= Time.deltaTime;
    }
  }
}
