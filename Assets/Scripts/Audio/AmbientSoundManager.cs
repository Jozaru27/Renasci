using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class AmbientSoundManager : MonoBehaviour
{
    public AudioSource[] audioSource;
   

    public List<AudioClip> ambientSoundsClips = new List<AudioClip>();

    int ambientSound;
    int length=3;

    public bool enableCombatMusic;

    public AudioClip combatMusic;

    float startVolume = 1;
    bool isPlaying=false;

    public static AmbientSoundManager Instance { get; private set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        //audioSource = this.gameObject.GetComponents<AudioSource>();

        ambientSound=Random.Range(0,ambientSoundsClips.Count);

        audioSource[0].clip=ambientSoundsClips[ambientSound];
        audioSource[0].Play();

        audioSource[1].clip = combatMusic;
        

    }

    void Update()
    {
        Debug.Log(enableCombatMusic);
        if(!audioSource[0].isPlaying && enableCombatMusic==false){
            ambientSound=Random.Range(0,ambientSoundsClips.Count);

            audioSource[0].clip=ambientSoundsClips[ambientSound];
             audioSource[0].Play();
        }else if(enableCombatMusic==true && !isPlaying){
            StopAllCoroutines();

            audioSource[0].Stop();

            StartCoroutine(FadeUp());

            isPlaying=true;
        }else if(enableCombatMusic==false && isPlaying){
            StopAllCoroutines();

            audioSource[1].Stop();

            StartCoroutine(FadeLow());

            isPlaying=false;
        }
    }

    IEnumerator FadeUp(){
        audioSource[1].Play();
        while(audioSource[1].volume<1){
            audioSource[0].volume -=0.1f;
            audioSource[1].volume +=0.1f;
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator FadeLow(){
        audioSource[0].Play();
        while(audioSource[0].volume<1){
            audioSource[1].volume -=0.1f;
            audioSource[0].volume +=0.1f;
            yield return new WaitForSeconds(0.25f);
        }
    }

   

  
}
