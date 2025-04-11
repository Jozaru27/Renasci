using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectScript : MonoBehaviour, IDamageable
{
    float health = 1.25f;
    public void TakeDamage(float amount, bool stateDamage)
    {
        health += amount;

        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        GetComponent<Renderer>().material.color = Color.red;

        yield return new WaitForSeconds(0.125f);

        GetComponent<Renderer>().material.color = Color.white;

        if (health <= 0)
            Destroy(this.gameObject);
    }
}
