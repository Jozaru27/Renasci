using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectScript : Attack,IDamageable
{
    public void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name=="HitTrigger" && Attack.currentRelic==Attack.Relics.Fire)
        {
           TakeDamage(100f,true);
        }
    }

    public void TakeDamage(float amount,bool stateDamage)
    {
        Destroy(this.gameObject);
    }
}
