using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundsManager : MonoBehaviour
{
    public AudioClip click;
    public AudioClip hover;

    public AudioSource audioSource;
    // Start is called before the first frame update
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource=this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playClickSound(){
        Debug.Log(click);
        audioSource.PlayOneShot(click,2f);
    }

    public void PlayHoverSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(hover,2f);
    }
}
