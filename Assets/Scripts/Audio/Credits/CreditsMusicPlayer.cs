using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMusicPlayer : MonoBehaviour
{
    [Header("Música de los créditos")]
    [SerializeField] private AudioClip creditsMusic;
    [SerializeField, Range(0f, 1f)] private float volume = 0.6f;
    [SerializeField] private bool loop = true;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = creditsMusic;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.Play();
    }
}
