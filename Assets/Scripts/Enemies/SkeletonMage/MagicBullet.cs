using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float damage;
    public float pushForce;

    Vector3 distanceToHead = new Vector3(0, 1.5f, 0);
    GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        StartCoroutine(Disappearing());
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position + distanceToHead, 2.5f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-damage, transform.position, pushForce);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(3);

        Destroy(this.gameObject);
    }
}
