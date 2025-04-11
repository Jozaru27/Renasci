using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRelicsManager : MonoBehaviour, ITakeable
{
    StatsManager stats;

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

        GameObject relicCanvas = Instantiate(UIManager.Instance.relicsUI, (transform.position + new Vector3(0, 2, 0)), Quaternion.identity);

        FloatingTextManager floatingTextManager = relicCanvas.transform.GetChild(0).GetComponent<FloatingTextManager>();

        if (life != 0) floatingTextManager.ShowFloatingText("Life + " + life, transform.position, relicCanvas);
        if (lifeRegeneration != 0) floatingTextManager.ShowFloatingText("Life Regen + " + lifeRegeneration, transform.position, relicCanvas);
        if (damage != 0) floatingTextManager.ShowFloatingText("Damage + " + damage, transform.position, relicCanvas);
        if (damageMultiplyer != 0) floatingTextManager.ShowFloatingText("Damage Multiplier + " + damageMultiplyer, transform.position, relicCanvas);
        if (criticalChance != 0) floatingTextManager.ShowFloatingText("Critical Chance + " + criticalChance, transform.position, relicCanvas);
        if (movementSpeed != 0) floatingTextManager.ShowFloatingText("Movement Speed + " + movementSpeed, transform.position, relicCanvas);
        if (attackSpeed != 0) floatingTextManager.ShowFloatingText("Attack Speed + " + attackSpeed, transform.position, relicCanvas);
        if (shootCadence != 0) floatingTextManager.ShowFloatingText("Shoot Cadence + " + shootCadence, transform.position, relicCanvas);
        if (dashCooldown != 0) floatingTextManager.ShowFloatingText("Dash Cooldown + " + dashCooldown, transform.position, relicCanvas);
        if (evasion != 0) floatingTextManager.ShowFloatingText("Evasion + " + evasion, transform.position, relicCanvas);

        Destroy(this.gameObject);
    }
}