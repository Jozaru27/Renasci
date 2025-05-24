using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip SpikeAttack;
    // public AudioClip SpikeHide;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpikeSound()
    {
        audioSource.PlayOneShot(SpikeAttack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-3, transform.position, 0);
    }
}
