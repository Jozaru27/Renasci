using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSoundEffect : MonoBehaviour
{
    [SerializeField] AudioClip waterClip;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(waterClip, 5f);
    }
}
