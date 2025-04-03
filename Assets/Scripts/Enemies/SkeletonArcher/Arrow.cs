using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    public float pushForce;
    private bool hasHitPlayer = false;
    private bool hasHitSomethingElse = false;

    private Collider arrowCollider;
    private Rigidbody arrowRb;
    private GameObject skeletonArcher;

    void Start()
    {
        arrowCollider = GetComponent<Collider>();
        arrowRb = GetComponent<Rigidbody>();

        skeletonArcher = GameObject.Find("SkeletonArcher"); 
        StartCoroutine(DisableCollisionTemporarily(0.5f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si la flecha colisiona con algo que no es el jugador, o ya ha golpeado al jugador, no le puede volver a dañar. Si es una superficie, se queda clavada [TO DO]
        if (hasHitPlayer || hasHitSomethingElse)
        {
            return;
        }

        // Si la flecha colisiona con el jugador antes que con cualquier otra cosa, aplica daño
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-damage, transform.position, pushForce);
            hasHitPlayer = true;
            Destroy(gameObject);
        }
        else
        {
            hasHitSomethingElse = true;
            StartCoroutine(DestroyAfterDelay(3f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // Desactiva la colisión entre la flecha y el SkeletonArcher durante un tiempo (para evitar la colisión inicial)
    private IEnumerator DisableCollisionTemporarily(float time)
    {
        if (skeletonArcher != null)
        {
            Collider skeletonCollider = skeletonArcher.GetComponent<Collider>();
            Physics.IgnoreCollision(arrowCollider, skeletonCollider, true);
        }

        yield return new WaitForSeconds(time);

        if (skeletonArcher != null)
        {
            Collider skeletonCollider = skeletonArcher.GetComponent<Collider>();
            Physics.IgnoreCollision(arrowCollider, skeletonCollider, false);
        }
    }
}