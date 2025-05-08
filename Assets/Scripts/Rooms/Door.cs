using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject[] colliders;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PauseAnimator()
    {
        anim.speed = 0f;
    }

    public void EnableColliders()
    {
        foreach (GameObject collider in colliders)
        {
            collider.SetActive(true);
        }
    }

    public void DisableColliders()
    {
        foreach (GameObject collider in colliders)
        {
            collider.SetActive(false);
        }
    }
}
