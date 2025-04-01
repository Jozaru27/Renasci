using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject[] loot;

    bool opened;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!opened)
        {
            anim.Play("ChestOpen");
            GameManager.Instance.playerCannotMove = true;
            opened = true;
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
