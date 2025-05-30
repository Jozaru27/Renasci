using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class MagicBullet : MonoBehaviour
{
    [SerializeField] VisualEffect orbVFX;
    [SerializeField] TrailRenderer trail;
    [SerializeField] GameObject collissionParticles;

    public float damage;
    public float pushForce;

    bool collided;

    Vector3 distanceToHead = new Vector3(0, 1.5f, 0);
    GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        StartCoroutine(Disappearing());
    }

    private void Update()
    {
        if (!collided) 
            transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position + distanceToHead, 2.5f * Time.deltaTime);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && !collided)
    //    {
    //        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-damage, transform.position, pushForce);
    //        StartCoroutine(CollideWithPlayer());
    //        collided = true;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !collided)
        {
            other.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-damage, transform.position, pushForce);
            StartCoroutine(CollideWithPlayer());
            collided = true;
        }
    }

    IEnumerator CollideWithPlayer()
    {
        float mainOrbSize = orbVFX.GetFloat("MainOrbSize");
        float secondaryOrbsCount = orbVFX.GetFloat("SecondaryOrbsCount");
        float trailTime = trail.time;

        float elapsedTime = 0f;

        collissionParticles.SetActive(true);
        collissionParticles.GetComponent<ParticleSystem>().Play();

        while (elapsedTime < 0.3f)
        {
            mainOrbSize -= 10f * Time.deltaTime;
            secondaryOrbsCount -= 100f * Time.deltaTime;
            trailTime -= 10f * Time.deltaTime;

            orbVFX.SetFloat("MainOrbSize", mainOrbSize);
            orbVFX.SetFloat("SecondaryOrbsCount", secondaryOrbsCount);
            trail.time = trailTime;

            if (orbVFX.GetFloat("MainOrbSize") < 0)
                orbVFX.SetFloat("MainOrbSize", 0);
            if (orbVFX.GetFloat("SecondaryOrbsCount") < 0)
                orbVFX.SetFloat("SecondaryOrbsCount", 0);
            if (trail.time < 0)
                trail.time = 0;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(this.gameObject);
    }

    IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(3f);

        float mainOrbSize = orbVFX.GetFloat("MainOrbSize");
        float secondaryOrbsCount = orbVFX.GetFloat("SecondaryOrbsCount");
        float trailTime = trail.time;

        while (orbVFX.GetFloat("MainOrbSize") > 0 && orbVFX.GetFloat("SecondaryOrbsCount") > 0)
        {
            mainOrbSize -= 2f * Time.deltaTime;
            secondaryOrbsCount -= 50f * Time.deltaTime;
            trailTime -= 2f * Time.deltaTime;

            orbVFX.SetFloat("MainOrbSize", mainOrbSize);
            orbVFX.SetFloat("SecondaryOrbsCount", secondaryOrbsCount);
            trail.time = trailTime;

            if (orbVFX.GetFloat("MainOrbSize") < 0)
                orbVFX.SetFloat("MainOrbSize", 0);
            if (orbVFX.GetFloat("SecondaryOrbsCount") < 0)
                orbVFX.SetFloat("SecondaryOrbsCount", 0);
            if (trail.time < 0)
                trail.time = 0;

            yield return null;
        }

        Destroy(this.gameObject);
    }
}
