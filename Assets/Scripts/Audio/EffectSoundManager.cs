using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class EffectSoundManager : MonoBehaviour
{
    AudioSource audioSource;

   public List<AudioClip> effectSoundsClips = new List<AudioClip>();

    int effectSound;
    int length=1;
    int chance1;
    int chance2;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();

        effectSound=Random.Range(0,effectSoundsClips.Count);
        

    }

    void Update()
    {
       
        if (!audioSource.isPlaying)
        {
            effectSound = Random.Range(0, effectSoundsClips.Count);
            chance1 = Random.Range(1, 101);
            
            if (chance1 == 5)
            {
                //Debug.Log("susurro susurrez");
                audioSource.clip = effectSoundsClips[effectSound];
                audioSource.Play();
            }
        }
    }
   
}
