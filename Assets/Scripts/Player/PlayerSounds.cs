using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip step1;
    public AudioClip step2;
    public AudioClip dash;
    public AudioClip attack;
    public AudioClip death;
    public AudioClip hit;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public void Step1Sound()
    {
        //audioSource.clip = step1;
        audioSource.PlayOneShot(step1);
    }

    public void Step2Sound()
    {
        //audioSource.clip = step2;
        audioSource.PlayOneShot(step2);
    }
    */
    
    public void playStepSounds()
    {
        int rand = Random.Range(1, 3);
        if (rand == 1)
        {
            audioSource.PlayOneShot(step1, 5f);
        }
        else
        {
            audioSource.PlayOneShot(step2, 5f);
        }
    }

    public void playDashSound()
    {
        //audioSource.Stop();
        audioSource.PlayOneShot(dash,5f);
    }

    public void playAttackSound()
    {
        //audioSource.Stop();
        audioSource.PlayOneShot(attack, 5f);
    }

    public void playDeathSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(death, 5f);
    }

    public void playHitSound()
    {
        audioSource.PlayOneShot(hit,5f);
    }
}
