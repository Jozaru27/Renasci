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

        if (chance <= 0.1f)
        {
            Debug.Log("Playing normal chest sound.");
            audioSource.PlayOneShot(Chest, 2f);
        }
        else
        {
            Debug.Log("🎉 Lucky! Playing special chest sound!");
            audioSource.PlayOneShot(ChestSpecial, 2f);
        }
    }
}
