using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] CinemachineVirtualCamera[] roomCameras;

    CinemachineVirtualCamera currentCamera;

    public static CameraManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentCamera = roomCameras[0];
    }

    public void ChangeToPlayerCamera()
    {
        currentCamera.Priority--;
        playerCamera.Priority++;
        currentCamera = playerCamera;
    }

    public void ChangeToRoomCamera(CinemachineVirtualCamera newCamera)
    {
        currentCamera.Priority--;
        newCamera.Priority++;
        currentCamera = newCamera;
    }
}
