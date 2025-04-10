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

    int prevEffectSound;
    bool onceAct = false;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();

        effectSound=Random.Range(0,effectSoundsClips.Count);
        prevEffectSound = effectSound;

        audioSource.clip = effectSoundsClips[effectSound];
        audioSource.Play();

    }

    private void Start()
    {
        
    }

    void Update()
    {
        chance1 = Random.Range(1, 101);

        if (!audioSource.isPlaying && onceAct == false)
        {
          
            StartCoroutine(playEffectSound());

            onceAct = true;
        }
        /*
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
        */
    }

    IEnumerator playEffectSound()
    {
     
        while (effectSound == prevEffectSound && chance1!=1)
        {
            chance1 = Random.Range(1, 101);
            effectSound = Random.Range(0, effectSoundsClips.Count);
        }
        prevEffectSound = effectSound;

        audioSource.clip = effectSoundsClips[effectSound];
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);
        yield return new WaitForSeconds(15f);
        StartCoroutine(playEffectSound());
    }
   
}
