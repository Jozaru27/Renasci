using UnityEngine;

public class AmbientLoopPlayer : MonoBehaviour
{
    [System.Serializable]
    public class AmbientSound
    {
        public AudioClip clip;
        public Vector3 positionOffset;
        public float volume = 1f;
        public bool is3D = true;
        public AudioReverbPreset reverb = AudioReverbPreset.Off;
    }

    [Header("Sonidos ambientales en loop")]
    public AmbientSound[] ambientSounds;

    void Start()
    {
        foreach (AmbientSound sound in ambientSounds)
        {
            if (sound.clip == null)
                continue;

            GameObject soundGO = new GameObject("Ambient_" + sound.clip.name);
            soundGO.transform.parent = transform;
            soundGO.transform.localPosition = sound.positionOffset;

            AudioSource source = soundGO.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.loop = true;
            source.volume = sound.volume;
            source.spatialBlend = sound.is3D ? 1f : 0f;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.minDistance = 1f;
            source.maxDistance = 10f;
            source.Play();

            if (sound.reverb != AudioReverbPreset.Off)
            {
                var reverb = soundGO.AddComponent<AudioReverbFilter>();
                reverb.reverbPreset = sound.reverb;
            }
        }
    }
}
