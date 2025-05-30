using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectScript : MonoBehaviour, IDamageable
{
    float health = 1.25f;
    bool damaged = false;

    [SerializeField] Material dissolveMaterial;
    [SerializeField] GameObject particles;

    public void TakeDamage(float amount, bool stateDamage)
    {
        health += amount;

        if (!damaged)
            StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve()
    {
        damaged = true;
        GetComponent<Renderer>().material = dissolveMaterial;
        particles.SetActive(true);
        particles.GetComponent<ParticleSystem>().Play();

        while (GetComponent<Renderer>().material.GetFloat("_CutOff_Height") > -80)
        {
            GetComponent<Renderer>().material.SetFloat("_CutOff_Height", GetComponent<Renderer>().material.GetFloat("_CutOff_Height") - (15f * Time.deltaTime));

            yield return null;
        }

        Destroy(this.gameObject);
    }
}
