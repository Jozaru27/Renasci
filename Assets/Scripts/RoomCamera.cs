using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera roomCamera;
    [SerializeField] Collider trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (roomCamera == null)
                CameraManager.Instance.ChangeCamerasReferences(null);
            else
                CameraManager.Instance.ChangeCamerasReferences(roomCamera);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CameraManager.Instance.ChangeRoomCamera();

            trigger.enabled = false;
            trigger.enabled = true;
        }
    }
}
