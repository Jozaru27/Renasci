using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WindRelic : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float impulseForce;

    Vector3 direction;

    List<GameObject> currentEnemies = new List<GameObject>();

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void GetDirection(Vector3 setDirection)
    {
        direction = setDirection.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        StartCoroutine(AttackDissappear());
        StartCoroutine(Pushing());
    }

    IEnumerator AttackDissappear()
    {
        yield return new WaitForSeconds(3);

        Destroy(this.gameObject);
    }

    IEnumerator Pushing()
    {
        while (true)
        {
            if (currentEnemies.Count != 0)
            {
                foreach (GameObject enemy in currentEnemies)
                {
                    if (enemy != null)
                        enemy.GetComponent<Rigidbody>().AddForce(direction * impulseForce, ForceMode.Acceleration);
                }
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<StateEffect>().GetPushed();
            currentEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<StateEffect>().StopPushing();
            currentEnemies.Remove(other.gameObject);
        }
    }
}
