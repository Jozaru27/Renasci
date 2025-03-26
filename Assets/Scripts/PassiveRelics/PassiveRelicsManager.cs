using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRelicsManager : MonoBehaviour
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
    // Start is called before the first frame update
    void Awake()
    {
        stats = FindObjectOfType<StatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stats.life += life;
            stats.lifeRegeneration += lifeRegeneration;
            stats.damage += damage;
            stats.damageMultiplyer += damageMultiplyer;
            stats.criticalChance += criticalChance;
            stats.movementSpeed += movementSpeed;
            stats.attackSpeed += attackSpeed;
            stats.shootCadence += shootCadence;
            stats.dashCooldown += dashCooldown;
            stats.evasion += evasion;
        }
        Destroy(this.gameObject);
    }
}
