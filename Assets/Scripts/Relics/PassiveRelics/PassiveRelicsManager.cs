using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRelicsManager : MonoBehaviour, ITakeable
{
    StatsManager stats;

    public string relicEffect;

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

    GameObject playerObj;

    void Awake()
    {
        stats = FindObjectOfType<StatsManager>();
        playerObj = GameObject.Find("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            OnPlayerTake();
    }

    public void OnPlayerTake()
    {
        stats.maxLife += ((life * 100) / 100);
        stats.lifeRegeneration += ((lifeRegeneration * 100) / 100);
        stats.damage += ((damage * 100) / 100);
        stats.damageMultiplyer += ((damageMultiplyer * 100) / 100);
        stats.criticalChance += ((criticalChance * 100) / 100);
        stats.movementSpeed += ((movementSpeed * 100) / 100);
        stats.attackSpeed += ((attackSpeed * 100) / 100);
        stats.shootCadence += ((shootCadence * 100) / 100);
        stats.dashCooldown += ((dashCooldown * 100) / 100);
        stats.evasion += ((evasion * 100) / 100);

        if (stats.dashCooldown < 0)
            stats.dashCooldown = 0;

        if (lifeRegeneration > 0)
            playerObj.GetComponent<PlayerHealth>().StartCoroutine(playerObj.GetComponent<PlayerHealth>().LifeRegeneration());

        GameObject relicCanvas = Instantiate(UIManager.Instance.relicsUI, (transform.position + new Vector3(0, 2, 0)), Quaternion.identity);

        FloatingTextManager floatingTextManager = relicCanvas.transform.GetChild(0).GetComponent<FloatingTextManager>();

        if (life != 0) floatingTextManager.ShowFloatingText(relicEffect + life, transform.position, relicCanvas);
        if (lifeRegeneration != 0) floatingTextManager.ShowFloatingText(relicEffect + lifeRegeneration, transform.position, relicCanvas);
        if (damage != 0) floatingTextManager.ShowFloatingText(relicEffect + damage, transform.position, relicCanvas);
        if (damageMultiplyer != 0) floatingTextManager.ShowFloatingText(relicEffect + damageMultiplyer, transform.position, relicCanvas);
        if (criticalChance != 0) floatingTextManager.ShowFloatingText(relicEffect + criticalChance, transform.position, relicCanvas);
        if (movementSpeed != 0) floatingTextManager.ShowFloatingText(relicEffect + movementSpeed, transform.position, relicCanvas);
        if (attackSpeed != 0) floatingTextManager.ShowFloatingText(relicEffect + attackSpeed, transform.position, relicCanvas);
        if (shootCadence != 0) floatingTextManager.ShowFloatingText(relicEffect + shootCadence, transform.position, relicCanvas);
        if (dashCooldown != 0) floatingTextManager.ShowFloatingText(relicEffect + dashCooldown, transform.position, relicCanvas);
        if (evasion != 0) floatingTextManager.ShowFloatingText(relicEffect + evasion, transform.position, relicCanvas);

        GetComponent<AddRelicInventory>().PassInfoToInventory();

        UIManager.Instance.ChangeLife();
    }

    public void ChangeEffect(string newEffect)
    {
        relicEffect = newEffect;
    }
}