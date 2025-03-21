using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    bool follow;
    bool canFollow = true;
    NavMeshAgent agent;
    GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (canFollow)
        {
            if (Vector3.Distance(playerObj.transform.position, transform.position) <= 10)
                follow = true;
            else
                follow = false;
        }
        
        agent.isStopped = !follow;

        if (GameManager.Instance.gameOver || GameManager.Instance.gameWin)
            agent.isStopped = true;

        agent.SetDestination(playerObj.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 impulseDirection = collision.gameObject.transform.position - transform.position;
            impulseDirection = new Vector3(impulseDirection.x, 0, impulseDirection.z);

            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-4);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(impulseDirection.normalized * 25, ForceMode.Impulse);

            StartCoroutine(AttackWait());
        }
    }

    IEnumerator AttackWait()
    {
        follow = false;
        canFollow = false;

        yield return new WaitForSeconds(1);

        canFollow = true;
    }
}
