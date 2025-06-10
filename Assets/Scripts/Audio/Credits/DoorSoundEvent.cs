using UnityEngine;

public class DoorSoundEvent : MonoBehaviour
{
    [Header("Sonido de la puerta")]
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioClip doorSound2;
    [SerializeField] private AudioClip doorSoundclose;
    [SerializeField, Range(0f, 1f)] private float volume = 0.7f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    public void PlayDoorSound()
    {
        audioSource.PlayOneShot(doorSound, volume);
    }

    public void PlayDoorSound2()
    {
        audioSource.PlayOneShot(doorSound2, volume);
    }

    public void PlayDoorSoundClose()
    {
        audioSource.PlayOneShot(doorSoundclose, volume);
    }


}
