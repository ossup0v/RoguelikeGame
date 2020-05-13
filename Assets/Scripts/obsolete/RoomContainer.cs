using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContainer : MonoBehaviour
{
  public GameObject[] LeftRooms;
  public GameObject[] RightRooms;
  public GameObject[] TopRooms;
  public GameObject[] BottomRooms;

  //all of room
  public Dictionary<RoomDirectionType, GameObject[]> AvailableRooms = new Dictionary<RoomDirectionType, GameObject[]>();
  //only r,l,t,b
  public GameObject СlosingRoomR;
  public GameObject СlosingRoomL;
  public GameObject СlosingRoomT;
  public GameObject СlosingRoomB;
  public Dictionary<RoomDirectionType, GameObject> СlosingRooms = new Dictionary<RoomDirectionType, GameObject>();

  public GameObject ClosedRoom;
  //only LRTB
  public GameObject CreatingRoom;

  public List<GameObject> Rooms;
  public int CountOfGeneretedRooms;
  public float WaitTime;
  private bool IsBossSpawned = false;
  public GameObject Boss;

  private void Start()
  {
    Invoke("Init", 0.1f);
  }

  private void Init()
  {
    AvailableRooms[RoomDirectionType.Left] = LeftRooms;
    AvailableRooms[RoomDirectionType.Right] = RightRooms;
    AvailableRooms[RoomDirectionType.Top] = TopRooms;
    AvailableRooms[RoomDirectionType.Bottom] = BottomRooms;
    AvailableRooms[RoomDirectionType.All] = new GameObject[] { CreatingRoom };
    СlosingRooms[RoomDirectionType.Left] = СlosingRoomL;
    СlosingRooms[RoomDirectionType.Right] = СlosingRoomR;
    СlosingRooms[RoomDirectionType.Top] = СlosingRoomT;
    СlosingRooms[RoomDirectionType.Bottom] = СlosingRoomB;
  }

  private void Update()
  {
    CountOfGeneretedRooms = Rooms.Count;
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
