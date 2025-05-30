using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CameraBlend : MonoBehaviour
{
    [Header("Camera Behaviour")]
    [SerializeField] float cameraStartingDistance;
    [SerializeField] float finalXRotation;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject distancedObj;
    [SerializeField] GameObject blendedCam;

    [Header("Door Behaviour")]
    [SerializeField] float doorStartingDistance;
    [SerializeField] GameObject[] doors;
    [SerializeField] float[] finalYRotations;


    float startingXRotation;
    float startingYRotation;

    void Start()
    {
        startingXRotation = blendedCam.transform.eulerAngles.x;
        startingYRotation = doors[0].transform.eulerAngles.y;
    }

    void Update()
    {
        float distanceToObj = Vector3.Distance(playerObj.transform.position, distancedObj.transform.position);

        CameraPosition(distanceToObj);
        DoorRotation(distanceToObj);
    }

    void CameraPosition(float distance)
    {
        if (distance <= cameraStartingDistance) 
        {
            float t = 1 - (distance / cameraStartingDistance);
            t = Mathf.Clamp01(t);

            float currentX = Mathf.LerpAngle(startingXRotation, finalXRotation, t);
            Vector3 currentEuler = blendedCam.transform.eulerAngles;
            blendedCam.transform.eulerAngles = new Vector3(currentX, currentEuler.y, currentEuler.z);

            //if (Mathf.Abs(Mathf.DeltaAngle(currentX, finalXRotation)) <= 0.1f)
            //    finishBlend = true;
        }
    }

    void DoorRotation(float distance)
    {
        if (distance <= doorStartingDistance)
        {
            float t = 1 - (distance / doorStartingDistance);
            t = Mathf.Clamp01(t);

            for(int i = 0; i < doors.Length; i++)
            {
                doors[i].transform.eulerAngles = new Vector3(doors[i].transform.eulerAngles.x, Mathf.LerpAngle(startingYRotation, finalYRotations[i] *  3, t), doors[i].transform.eulerAngles.z);
            }
        }
    }
}
