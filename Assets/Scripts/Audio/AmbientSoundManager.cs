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

    public bool enableCombatMusic = false;

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
        }else if (enableCombatMusic == true && !isPlaying)
        {
            StartCoroutine(Fade());

            isPlaying = true;
        }
    }

   

   public void EnableCombatMusic()
    {
        enableCombatMusic = true;
    }

    IEnumerator Fade()
    {
        audioSource[0].volume -= 0.25f;
        audioSource[1].volume += 0.25f;
        if (audioSource[0].volume == 0)
        {
            StopCoroutine(Fade());
        }
        yield return new WaitForSeconds(0.5f);
    }
}
