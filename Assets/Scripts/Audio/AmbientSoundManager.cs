using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class AmbientSoundManager : MonoBehaviour
{
    AudioSource audioSource;

   public List<AudioClip> ambientSoundsClips = new List<AudioClip>();

    int ambientSound;
    int length=3;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();

        ambientSound=Random.Range(0,ambientSoundsClips.Count);

        audioSource.clip=ambientSoundsClips[ambientSound];
        audioSource.Play();
    }

    void update()
    {
        if(!audioSource.isPlaying){
            ambientSound=Random.Range(0,ambientSoundsClips.Count);

            audioSource.clip=ambientSoundsClips[ambientSound];
             audioSource.Play();
        }
    }
   
}
