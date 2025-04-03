using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip step1;
    public AudioClip step2;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Step1Sound()
    {
        //audioSource.clip = step1;
        audioSource.PlayOneShot(step1);
    }

    void Step2Sound()
    {
        //audioSource.clip = step2;
        audioSource.PlayOneShot(step2);
    }
}
