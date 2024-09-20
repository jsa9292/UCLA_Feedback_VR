using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSetup : MonoBehaviour
{
    public MovingWall[] mws;
    public GameObject[] rooms;
    public Vector3 roomPos = Vector3.zero;
    // Start is called before the first frame update
    void CalculateRoomPos() {
        roomPos = Vector3.zero;
        foreach (MovingWall wall in mws) {
            roomPos += wall.transform.position;
        }
        roomPos /= 2f;
        roomPos.y = 0f;
    }
    public void ActivateRoom(int i) {
        CalculateRoomPos();
        foreach (GameObject room in rooms) {
            room.SetActive(false);
        }
        if (i < 0)
        {
            gameObject.SetActive(true);
            return;
        }
        rooms[i].SetActive(true);
        rooms[i].transform.position = roomPos;
        gameObject.SetActive(false);
    }
}
