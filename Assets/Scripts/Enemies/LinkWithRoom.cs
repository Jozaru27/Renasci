using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkWithRoom : MonoBehaviour
{
    public RoomCamera roomCam;

    public void RemoveFromRoomList()
    {
        roomCam.RemoveEnemy(this.gameObject);
    }
}
