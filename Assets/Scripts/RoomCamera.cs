using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomCamera : MonoBehaviour
{
    public bool entered;
    public bool comeOut;

    [SerializeField] CinemachineVirtualCamera roomCamera;
    [SerializeField] Collider trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!comeOut && !CameraManager.Instance.firstTime)
            {
                entered = true;

                if (roomCamera == null)
                    CameraManager.Instance.ChangeCamerasReferences(null, this);
                else
                    CameraManager.Instance.ChangeCamerasReferences(roomCamera, this);
            }

            comeOut = false;
            CameraManager.Instance.firstTime = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!entered && !CameraManager.Instance.firstTime)
            {
                comeOut = true;

                CameraManager.Instance.ChangeRoomCamera(this);

                trigger.enabled = false;
                trigger.enabled = true;
            }

            entered = false;
            CameraManager.Instance.firstTime = false;
        }
    }
}
