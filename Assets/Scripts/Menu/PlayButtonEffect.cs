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

    float startingYRotation;

    private void Start()
    {
        startingYRotation = doors[0].transform.localEulerAngles.y;
    }

    public void OnPlay() 
    { 
        foreach(Button currentButton in menuButtons)
        {
            currentButton.interactable = false;
        }

        fadeImg.gameObject.SetActive(true);

        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        float timeElapsed = 0f;

        Color imageColor = fadeImg.color;
        imageColor.a = fadeImg.color.a;
        int iterations = 0;

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
            }

            iterations++;

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
