using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectScript : Attack,IDamageable
{
    float health = 1.25f;
    /*
    public void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name=="HitTrigger" && Attack.currentRelic==Attack.Relics.Fire)
        {
          
        }
    }
    */
    public void TakeDamage(float amount, bool stateDamage)
    {
        health += amount;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
}
