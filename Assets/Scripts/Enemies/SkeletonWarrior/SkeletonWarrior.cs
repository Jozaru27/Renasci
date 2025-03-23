using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour, IDamageable
{
    NavMeshAgent agent;
    SkeletonWarriorStates FSM;

    public EnemyStats stats;
    public GameObject playerObject;
    public GameObject skeletonWarriorObject;
    public Animator skeletonWarriorAnimator;

    public bool isBlocking=false;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        agent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");
        skeletonWarriorObject = this.gameObject;
        skeletonWarriorAnimator=skeletonWarriorObject.GetComponent<Animator>();

        agent.speed = stats.movementSpeed;
      
        FSM = new SkeletonWarriorIdle(this);
    }

    void Update()
    {
        FSM = FSM.Process();
    }

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("A");
            other.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-stats.mainDamage, skeletonWarriorObject.transform.position, stats.pushForce);
        }
    }
    
    public void TakeDamage(float amount)
    {
        if(isBlocking==false){
            stats.life += amount;
        }else{
            Debug.Log("Blocked");
        }

        if (stats.life <= 0)
        {
            stats.life = 0;
            Destroy(this.gameObject);
        }
    }
}
