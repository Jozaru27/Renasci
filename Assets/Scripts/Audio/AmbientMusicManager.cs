using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class AmbientMusicManager : MonoBehaviour
{
    public AudioSource[] audioSource;

    public List<AudioClip> ambientSoundsClips = new List<AudioClip>();

    int ambientSound;
    int length=3;

    public bool enableCombatMusic;

    public AudioClip combatMusic;

    int enemiesInCombat = 0;
    float startVolume = 1;
    bool audioOnePlaying = false;
    bool audioTwoPlaying = false;

    public static AmbientMusicManager Instance { get; private set; }
    
    void Awake()
    {
        Instance = this;

        ambientSound=Random.Range(0,ambientSoundsClips.Count);

        audioSource[0].clip=ambientSoundsClips[ambientSound];
        audioSource[0].Play();

        audioSource[1].clip = combatMusic;
        audioSource[1].Play();
    }

    void Update()
    {
        if(enableCombatMusic == true && !audioTwoPlaying)
        {
            StopAllCoroutines();
            StartCoroutine(FadeUp());

            audioOnePlaying = false;
            audioTwoPlaying = true;
        }
        else if(enableCombatMusic == false && !audioOnePlaying)
        {
            StopAllCoroutines();
            StartCoroutine(FadeLow());

            audioOnePlaying = true;
            audioTwoPlaying = false;
        }
    }

    IEnumerator FadeUp()
    {
        while(audioSource[1].volume < 1)
        {
            audioSource[0].volume -= (0.25f * Time.deltaTime);
            audioSource[1].volume += (0.25f * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator FadeLow()
    {
        while(audioSource[0].volume < 1)
        {
            audioSource[1].volume -= (0.25f * Time.deltaTime);
            audioSource[0].volume += (0.25f * Time.deltaTime);
            yield return null;
        }
    }

    public void EnterCombatMode()
    {
        enemiesInCombat++;

        if (enemiesInCombat > 0)
            enableCombatMusic = true;
        if (enemiesInCombat >= GameManager.Instance.enemies.Length)
            enemiesInCombat = GameManager.Instance.enemies.Length;
    }

    public void ExitCombatMode()
    {
        enemiesInCombat--;

        if (enemiesInCombat <= 0)
        {
            enemiesInCombat = 0;
            enableCombatMusic = false;
        }
    }
}
