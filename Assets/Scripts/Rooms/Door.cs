using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isBig;
    [SerializeField] GameObject[] colliders;
    [SerializeField] GameObject dustParticle;

    [Header("Audio")]
    [SerializeField] private AudioClip sonidoAbrir;
    [SerializeField] private AudioClip sonidoCerrar;
    private AudioSource audioSource;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

        public void AbrirPuerta()
    {
        if (isBig)
        {
            anim.SetTrigger("Door_Open_Big");
        }
        else
        {
            anim.SetTrigger("Door_Open");
        }

        audioSource.PlayOneShot(sonidoAbrir);
    }

    public void CerrarPuerta()
    {
        if (isBig)
        {
            anim.SetTrigger("Door_Close_Big");
        }
        else
        {
            anim.SetTrigger("Door_Close");
        }

        audioSource.PlayOneShot(sonidoCerrar);
    }
}
