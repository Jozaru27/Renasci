using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] CinemachineVirtualCamera firstRoomCamera;

    CinemachineVirtualCamera currentCamera;
    CinemachineVirtualCamera nextCamera;

    public static CameraManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentCamera = firstRoomCamera;
    }

    public void ChangeCamerasReferences(CinemachineVirtualCamera newCamera)
    {
        if (newCamera == null)
            newCamera = playerCamera;

        nextCamera = newCamera;
    }

    public void ChangeRoomCamera()
    {
        currentCamera.Priority--;
        nextCamera.Priority++;
        currentCamera = nextCamera;
    }
}
