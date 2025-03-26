using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawn : MonoBehaviour
{
    [SerializeField] GameObject healObj;

    public void SpawnHeal()
    {
        int spawnRate = Random.Range(0, 3);

        if (spawnRate == 0)
            Instantiate(healObj, transform.position, Quaternion.identity);
    }
}
