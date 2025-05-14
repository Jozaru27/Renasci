using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isBig;
    [SerializeField] GameObject[] colliders;
    [SerializeField] GameObject dustParticle;

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

    public void GenerateDust()
    {
        dustParticle.SetActive(true);
        dustParticle.GetComponent<ParticleSystem>().Play();
    }
}
