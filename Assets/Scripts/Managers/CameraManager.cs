using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public bool firstTime = true;

    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] CinemachineVirtualCamera firstRoomCamera;

    CinemachineVirtualCamera currentCamera;
    CinemachineVirtualCamera nextCamera;

    RoomCamera enteredCamera, comeOutCamera;

    public static CameraManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentCamera = firstRoomCamera;
    }

    public void ChangeCamerasReferences(CinemachineVirtualCamera newCamera, RoomCamera entered)
    {
        if (newCamera == null)
            newCamera = playerCamera;

        nextCamera = newCamera;

        enteredCamera = entered;

        if (comeOutCamera != null && comeOutCamera.comeOut)
            ResetVariables();
    }

    public void ChangeRoomCamera(RoomCamera comeOut)
    {
        currentCamera.Priority--;
        nextCamera.Priority++;
        currentCamera = nextCamera;

        comeOutCamera = comeOut;

        if (enteredCamera != null && enteredCamera.entered)
            ResetVariables();
    }

    void ResetVariables()
    {
        comeOutCamera.comeOut = false;
        comeOutCamera.entered = false;
        enteredCamera.entered = false;
        enteredCamera.comeOut = false;
    }
}
