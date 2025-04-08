using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera roomCamera;

    public enum RoomType
    {
        FreeCamera,
        StaticCamera
    }
    public RoomType room;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (room == RoomType.FreeCamera)
                CameraManager.Instance.ChangeToPlayerCamera();
            else
                CameraManager.Instance.ChangeToRoomCamera(roomCamera);
        }
    }
}
