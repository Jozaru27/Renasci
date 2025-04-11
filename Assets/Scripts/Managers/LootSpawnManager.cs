using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawnManager : MonoBehaviour
{
    [SerializeField] float lootProbability;
    [SerializeField] float healProbability;
    [SerializeField] GameObject heal;
    [SerializeField] GameObject[] relics;

    Vector3 currentSpawnPosition;

    public static LootSpawnManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void LootProbability(Vector3 spawnPosition)
    {
        currentSpawnPosition = spawnPosition;
        float randomNum = Random.Range(0, 100);

        if (randomNum <= lootProbability)
            HealProbability();
    }

    void HealProbability()
    {
        float randomNum = Random.Range(0, 100);

        if (randomNum <= healProbability)
            SpawnHeal();
        else
            RelicProbability();
    }

    void RelicProbability()
    {
        int relicNum = 0;
        float randomNum = Random.Range(0, 100);
        float percentage = 100 / relics.Length;
        
        while (randomNum > percentage)
        {
            relicNum++;
            percentage += percentage;
        }

        SpawnRelic(relicNum);
    }

    void SpawnHeal()
    {
        Instantiate(heal, currentSpawnPosition, Quaternion.identity);
    }

    void SpawnRelic(int relicNum)
    {
        Instantiate(relics[relicNum], currentSpawnPosition, Quaternion.identity);
    }
}
