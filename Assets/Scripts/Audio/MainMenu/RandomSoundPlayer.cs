using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] soundClips;

    public float minDelay = 15f;
    public float maxDelay = 30f;

    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);
    public Vector2 volumeRange = new Vector2(0.8f, 1f);

    public float radius = 5f;
    public AudioReverbPreset reverbPreset = AudioReverbPreset.Off;

    private int lastIndex = -1;

    void Start()
    {
        StartCoroutine(PlayRandomSounds());
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            int index;
            do
            {
                index = Random.Range(0, soundClips.Length);
            } while (index == lastIndex && soundClips.Length > 1);

            lastIndex = index;

            Play3DSound(soundClips[index]);
        }
    }

    void Play3DSound(AudioClip clip)
    {
        Debug.Log("PLAYING");
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = transform.position + Random.onUnitSphere * radius;
        tempGO.transform.LookAt(transform);

        AudioSource tempAS = tempGO.AddComponent<AudioSource>();
        tempAS.clip = clip;
        tempAS.spatialBlend = 1f;
        tempAS.rolloffMode = AudioRolloffMode.Linear;
        tempAS.minDistance = 1f;
        tempAS.maxDistance = radius + 2f;
        tempAS.volume = Random.Range(volumeRange.x, volumeRange.y);
        tempAS.pitch = Random.Range(pitchRange.x, pitchRange.y);
        tempAS.Play();

        if (reverbPreset != AudioReverbPreset.Off)
        {
            AudioReverbFilter reverb = tempGO.AddComponent<AudioReverbFilter>();
            reverb.reverbPreset = reverbPreset;
        }

        Destroy(tempGO, clip.length + 1f); 
    }
}
