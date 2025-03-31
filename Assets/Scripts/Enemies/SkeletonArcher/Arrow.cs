using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    public float pushForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-damage, transform.position, pushForce);
    }
}