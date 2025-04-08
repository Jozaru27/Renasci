/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class AmbientSoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public string folder = "Assets/Sounds/AmbientSounds";

    AudioClip[] ambientSoundsClips;
    string[] ambientSoundsFiles;

    int ambientSoundClip;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        ambientSoundsFiles = Directory.GetFiles(folder,"*mp3"); //obtenemos las rutas de los archivos y las guardamos en un array

        ambientSoundsClips = new AudioClip[ambientSoundsFiles.Length];

        for (int i = 0; i < ambientSoundsFiles.Length; i++)
        {
            string archivePath = ambientSoundsFiles[i];
            StartCoroutine(LoadAudioClip(archivePath,i));
        }
    }

    void Start()
    {
        ambientSoundClip = Random.Range(0,ambientSoundsClips.Length);

        Debug.Log(ambientSoundsClips[ambientSoundClip]);

        audioSource.clip = ambientSoundsClips[ambientSoundClip];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            ambientSoundClip = Random.Range(0, ambientSoundsClips.Length);
        }
    }

    System.Collections.IEnumerator LoadAudioClip(string archivePath,int index)
    {
        string filePath = "file:///" + archivePath.Replace("\\", "/");

        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(filePath,AudioType.MPEG); //se le hace un apeticion local para obtener el clip que se encuentra en la ruta
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success)
        {
            ambientSoundsClips[index] = DownloadHandlerAudioClip.GetContent(request); //si obtiene el audioclip con exito, lo guarda en  array
        }
        else
        {
            Debug.Log("ERROR: AMBIENT SOUND COULDNT BE LOADED");
        }
    }
        //AudioType audioType = AudioType.MPEG; 
        //if (archivePath.EndsWith(".wav"))
        //{
            //audioType = AudioType.WAV; 
        //}
}
*/
