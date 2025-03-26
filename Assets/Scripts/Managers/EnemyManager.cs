using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;

    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            foreach (EnemyStatsScriptableObject stat in StatsManager.Instance.enemyStats)
            {
                if (stat.enemyName == enemy.GetComponent<EnemyStats>().enemy.ToString())
                {
                    enemy.GetComponent<EnemyStats>().enemyName = stat.enemyName;
                    enemy.GetComponent<EnemyStats>().life = stat.life;
                    enemy.GetComponent<EnemyStats>().mainDamage = stat.mainDamage;
                    enemy.GetComponent<EnemyStats>().secondaryDamage = stat.secondaryDamage;
                    enemy.GetComponent<EnemyStats>().heal = stat.heal;
                    enemy.GetComponent<EnemyStats>().movementSpeed = stat.movementSpeed;
                    enemy.GetComponent<EnemyStats>().actionSpeed = stat.actionSpeed;
                    enemy.GetComponent<EnemyStats>().pushForce = stat.pushForce;
                    enemy.GetComponent<EnemyStats>().detectionDistance = stat.detectionDistance;
                    enemy.GetComponent<EnemyStats>().pushedForce = stat.pushedForce;
                }
            }
        }
    }
}
