using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject[] loot;

    bool opened;
    Animator anim;
    GameObject interactUI;
    Image interactImage;
    TMP_Text interactText;

    private void Start()
    {
        anim = GetComponent<Animator>();
        interactUI = GameObject.Find("--INTERACT_UI--");
        interactImage = GameObject.Find("InteractionImage").GetComponent<Image>();
        interactText = GameObject.Find("InteractionText").GetComponent<TMP_Text>();
    }

    public void Hold()
    {
        if (!opened)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnInteractUI());
        }
    }

    public void Unhold()
    {
        if (!opened)
        {
            StopAllCoroutines();
            StartCoroutine(DespawnInteractUI());
        }
    }

    IEnumerator SpawnInteractUI()
    {
        interactUI.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        Color fadeColor = Color.white;
        fadeColor.a = interactImage.color.a;
        interactImage.color = fadeColor;
        interactText.color = fadeColor;

        while (interactImage.color.a < 1)
        {
            fadeColor.a += 2 * Time.unscaledDeltaTime;
            interactImage.color = fadeColor;
            interactText.color = fadeColor;

            yield return null;
        }
    }

    IEnumerator DespawnInteractUI()
    {
        Color fadeColor = Color.white;
        fadeColor.a = interactImage.color.a;
        interactImage.color = fadeColor;
        interactText.color = fadeColor;

        while (interactImage.color.a > 0)
        {
            fadeColor.a -= 2 * Time.unscaledDeltaTime;
            interactImage.color = fadeColor;
            interactText.color = fadeColor;

            yield return null;
        }
    }

    public void Interact()
    {
        if (!opened)
        {
            anim.Play("ChestOpen");
            GameManager.Instance.playerCannotMove = true;
            opened = true;
            StartCoroutine(DespawnInteractUI());
        }
    }

    public void OnChestOpened()
    {
        GameManager.Instance.playerCannotMove = false;

        int randomNum = Random.Range(0, loot.Length);
        GameObject generatedLoot = Instantiate(loot[randomNum], transform.position, Quaternion.identity);
        generatedLoot.GetComponent<ITakeable>().OnPlayerTake();
    }
}
