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

    public bool enableCombatMusic = false;

    public AudioClip combatMusic;

    float startVolume = 1;

    public static AmbientSoundManager Instance { get; private set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        audioSource = this.gameObject.GetComponent<AudioSource>();

        ambientSound=Random.Range(0,ambientSoundsClips.Count);

        audioSource.clip=ambientSoundsClips[ambientSound];
        audioSource.Play();

    }

    void Update()
    {
        Debug.Log(enableCombatMusic);
        if(!audioSource.isPlaying && enableCombatMusic==false){
            ambientSound=Random.Range(0,ambientSoundsClips.Count);

            audioSource.clip=ambientSoundsClips[ambientSound];
             audioSource.Play();
        }else if (enableCombatMusic == true)
        {
            
            StartCoroutine(volumeFadeLow());
            StopCoroutine(volumeFadeLow());
            audioSource.clip = combatMusic;
            audioSource.Play();
            StartCoroutine(volumeFadeUp());
            StopCoroutine(volumeFadeUp());

        }
    }

    IEnumerator volumeFadeLow()
    {
        Debug.Log("se baja el volumen");
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.5f;
            yield return new WaitForSeconds(1f);
        }
        
        
    }

    IEnumerator volumeFadeUp()
    {
        Debug.Log("se sube el volumen");
        while (audioSource.volume != startVolume)
        {
            audioSource.volume += 0.5f;
            yield return new WaitForSeconds(1f);
        }
    }

   public void EnableCombatMusic()
    {
        enableCombatMusic = true;
    }
}
