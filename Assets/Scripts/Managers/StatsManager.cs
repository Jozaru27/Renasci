using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [Header("PlayerCurrentStats")]
    public float life;
    public float lifeRegeneration;
    public float damage;
    public float damageMultiplyer;
    public float criticalChance;
    public float movementSpeed;
    public float attackSpeed;
    public float shootCadence;
    public float dashCooldown;
    public float evasion;

    [Header("EnemyStats")]
    public ScriptableObject[] enemyStats;

    public static StatsManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
