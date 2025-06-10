using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonEffect : MonoBehaviour
{
    [SerializeField] Button[] menuButtons;
    [SerializeField] Image fadeImg;
    [SerializeField] GameObject[] doors;
    [SerializeField] float[] finalYRotations;

    [Header("Sonidos")]
    [SerializeField] AudioClip doorSound;
    [SerializeField] AudioClip fadeSound;
    [SerializeField, Range(0f, 1f)] float doorVolume = 0.25f;
    [SerializeField, Range(0f, 1f)] float fadeVolume = 0.1f;

    AudioSource audioSource;
    float startingYRotation;

    private void Start()
    {
        startingYRotation = doors[0].transform.localEulerAngles.y;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPlay() 
    { 
        foreach(Button currentButton in menuButtons)
        {
            currentButton.interactable = false;
        }

        fadeImg.gameObject.SetActive(true);

        audioSource.PlayOneShot(doorSound, doorVolume);

        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        float timeElapsed = 0f;

        Color imageColor = fadeImg.color;
        imageColor.a = fadeImg.color.a;

        while (fadeImg.color.a < 1)
        {
            timeElapsed += Time.deltaTime;

            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].transform.localEulerAngles = new Vector3(doors[i].transform.localEulerAngles.x, Mathf.LerpAngle(startingYRotation, finalYRotations[i], timeElapsed * 0.25f), doors[i].transform.localEulerAngles.z);
            }

            if (timeElapsed > 1.5f)
            {
                imageColor.a += 1 * 0.35f * Time.deltaTime;
                fadeImg.color = imageColor;
                audioSource.PlayOneShot(fadeSound, fadeVolume);
            }

            yield return null;
        }

        timeElapsed = 0f;

        imageColor.a = 1;
        fadeImg.color = imageColor;

        while (fadeImg.color != Color.black)
        {
            timeElapsed += Time.deltaTime;

            fadeImg.color = Color.Lerp(Color.white, Color.black, timeElapsed * 0.5f);

            yield return null;
        }

        SceneLoader.Instance.LoadNextSceneAsync();
    }
}
