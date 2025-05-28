using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CreditScene : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera firstCam;
    [SerializeField] CinemachineVirtualCamera lastCam;

    private void Start()
    {
        StartCoroutine(ChangeCamPriorities());
        StartCoroutine(GoToMainMenu());
    }

    IEnumerator ChangeCamPriorities()
    {
        yield return null;

        firstCam.Priority--;
        lastCam.Priority++;
    }

    IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(60f);

        SceneLoader.Instance.LoadMainMenuAsync();
    }
}
