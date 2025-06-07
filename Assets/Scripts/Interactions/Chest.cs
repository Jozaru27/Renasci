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

    private void Start()
    {
        anim = GetComponent<Animator>();
        interactUI = GameObject.Find("--INTERACT_UI--");
    }

    public void Hold()
    {
        if (!opened)
        {
            interactUI.GetComponent<InteractableText>().Holding();
            interactUI.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        }
    }

    public void Unhold()
    {
        if (!opened)
            interactUI.GetComponent<InteractableText>().Unholding();
    }

    public void Interact()
    {
        if (!opened)
        {
            anim.Play("ChestOpen");
            GameManager.Instance.playerCannotMove = true;
            opened = true;
            interactUI.GetComponent<InteractableText>().Unholding();
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
