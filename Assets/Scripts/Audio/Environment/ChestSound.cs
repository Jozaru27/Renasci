using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSound : MonoBehaviour
{
    public AudioClip Chest;
    public AudioClip ChestSpecial;
    public AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayChestSound()
    {
        float chance = Random.Range(0f, 1f); 

        if (chance <= 0.99f)
        {
            audioSource.PlayOneShot(Chest, 2f);
        }
        else
        {
            audioSource.PlayOneShot(ChestSpecial, 2f);
        }
    }
}
