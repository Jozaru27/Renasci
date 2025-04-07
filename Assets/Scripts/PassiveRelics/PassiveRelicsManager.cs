using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRelicsManager : MonoBehaviour, ITakeable
{
    StatsManager stats;
    FloatingTextManager floatingTextManager;

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

    void Awake()
    {
        stats = FindObjectOfType<StatsManager>();

        floatingTextManager = FindObjectOfType<FloatingTextManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerTake();
        }
    }

    public void OnPlayerTake()
    {
        stats.maxLife += life;
        stats.lifeRegeneration += lifeRegeneration;
        stats.damage += damage;
        stats.damageMultiplyer += damageMultiplyer;
        stats.criticalChance += criticalChance;
        stats.movementSpeed += movementSpeed;
        stats.attackSpeed += attackSpeed;
        stats.shootCadence += shootCadence;
        stats.dashCooldown += dashCooldown;
        stats.evasion += evasion;

        if (floatingTextManager != null)
        {
            if (life != 0) floatingTextManager.ShowFloatingText("Life + " + life, transform.position);
            if (lifeRegeneration != 0) floatingTextManager.ShowFloatingText("Life Regen + " + lifeRegeneration, transform.position);
            if (damage != 0) floatingTextManager.ShowFloatingText("Damage + " + damage, transform.position);
            if (damageMultiplyer != 0) floatingTextManager.ShowFloatingText("Damage Multiplier + " + damageMultiplyer, transform.position);
            if (criticalChance != 0) floatingTextManager.ShowFloatingText("Critical Chance + " + criticalChance, transform.position);
            if (movementSpeed != 0) floatingTextManager.ShowFloatingText("Movement Speed + " + movementSpeed, transform.position);
            if (attackSpeed != 0) floatingTextManager.ShowFloatingText("Attack Speed + " + attackSpeed, transform.position);
            if (shootCadence != 0) floatingTextManager.ShowFloatingText("Shoot Cadence + " + shootCadence, transform.position);
            if (dashCooldown != 0) floatingTextManager.ShowFloatingText("Dash Cooldown + " + dashCooldown, transform.position);
            if (evasion != 0) floatingTextManager.ShowFloatingText("Evasion + " + evasion, transform.position);
        }

        Destroy(this.gameObject);
    }
}