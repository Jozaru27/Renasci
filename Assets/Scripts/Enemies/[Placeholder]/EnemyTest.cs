using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] GameObject heal;

    int life = 3;

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

    public void TakeDamage(int amount)
    {
        life += amount;

        if (life <= 0)
        {
            life = 0;
            Instantiate(heal, transform.position, Quaternion.identity);
            UIManager.Instance.ChangeEnemyCount();
            Destroy(this.gameObject);
        }

        StartCoroutine(AttackWait());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-4, transform.position, 25);
            
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
