using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EffectSoundManager : MonoBehaviour
{
    [Header("Lista de efectos de sonido")]
    [SerializeField] private List<AudioClip> effectSoundsClips = new List<AudioClip>();

    [Header("Tiempo entre sonidos")]
    [SerializeField] private float delayBetweenSounds = 15f;

    [Header("Volumen del sonido")]
    [SerializeField, Range(0f, 1f)] private float volume = 0.8f;

    private AudioSource audioSource;
    private int previousIndex = -1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f; 
        audioSource.playOnAwake = false;

        if (effectSoundsClips.Count == 0)
        {
            Debug.LogWarning("sfx list VACÍA");
            return;
        }

        StartCoroutine(PlayEffectSoundsLoop());
    }

    IEnumerator PlayEffectSoundsLoop()
    {
        while (true)
        {
            // Espera entre sonidos
            yield return new WaitForSeconds(delayBetweenSounds);

            // Teletransporte a la cámara principal
            if (Camera.main != null)
                transform.position = Camera.main.transform.position;

            // Selección aleatoria sin repetir el anterior
            int index;
            do
            {
                index = Random.Range(0, effectSoundsClips.Count);
            } while (index == previousIndex);

            previousIndex = index;

            // Reproducir sonido
            audioSource.PlayOneShot(effectSoundsClips[index], volume);

            // Esperar a que termine el sonido
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
}
