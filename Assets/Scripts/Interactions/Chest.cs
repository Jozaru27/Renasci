using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject[] loot;

    bool opened;

    public void Interact()
    {
        if (!opened)
        {
            int randomNum = Random.Range(0, loot.Length);

            GameObject generatedLoot = Instantiate(loot[randomNum], transform.position, Quaternion.identity);
            generatedLoot.GetComponent<ITakeable>().OnPlayerTake();

            opened = true;
        }
    }
}
