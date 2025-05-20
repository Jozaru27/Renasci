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
    [SerializeField] GameObject[] doors;

    [SerializeField] List <GameObject> enemies = new List <GameObject>();

    private void Start()
    {
        if (doors.Length > 0)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<Animator>().speed = 0;
            }
        }

        if (enemies.Count > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<LinkWithRoom>().roomCam = this;
            }
        }
    }

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

    public void EnableCombatMode()
    {
        if (enemies.Count > 0 && doors.Length > 0)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<Animator>().speed = 1;

                if (!door.GetComponent<Door>().isBig)
                    door.GetComponent<Animator>().Play("Door_Close");
                else
                    door.GetComponent<Animator>().Play("Door_Close_Big");
            }
        }
    }

    public void DisableCombatMode()
    {
        if (doors.Length > 0)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<Animator>().speed = 1;

                if (!door.GetComponent<Door>().isBig)
                    door.GetComponent<Animator>().Play("Door_Open");
                else
                    door.GetComponent<Animator>().Play("Door_Open_Big");
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count <= 0)
            DisableCombatMode();
    }
}
