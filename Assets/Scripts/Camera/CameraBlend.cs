using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CameraBlend : MonoBehaviour
{
    [SerializeField] float startingDistance;
    [SerializeField] float finalXRotation;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject distancedObj;
    [SerializeField] CinemachineVirtualCamera blendedCam;

    float startingXRotation;
    bool finishBlend;

    void Start()
    {
        startingXRotation = blendedCam.transform.eulerAngles.x;
    }

    void Update()
    {
        CameraPosition();
        DoorRotation();
    }

    void CameraPosition()
    {
        float distanceToObj = Vector3.Distance(playerObj.transform.position, distancedObj.transform.position);

        if (distanceToObj <= startingDistance && !finishBlend) 
        {
            float t = 1 - (distanceToObj / startingDistance);
            t = Mathf.Clamp01(t);

            float currentX = Mathf.LerpAngle(startingXRotation, finalXRotation, t);
            Vector3 currentEuler = blendedCam.transform.eulerAngles;
            blendedCam.transform.eulerAngles = new Vector3(currentX, currentEuler.y, currentEuler.z);

            if (Mathf.Abs(Mathf.DeltaAngle(currentX, finalXRotation)) <= 0.1f)
                finishBlend = true;
        }
    }

    void DoorRotation()
    {

    }
}
