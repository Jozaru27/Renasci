using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] GameObject sfd;

    void Start()
    {
        SceneLoader.Instance.StartLoading();
    }
}
