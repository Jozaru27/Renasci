using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CreditScene : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera firstCam;
    [SerializeField] CinemachineVirtualCamera lastCam;

    private void Start()
    {
        firstCam.Priority--;
        lastCam.Priority++;
    }
}
