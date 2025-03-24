using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour, IDamageable
{
    public NavMeshAgent skeletonWarriorAgent;
    SkeletonWarriorStates FSM;

    public EnemyStats stats;
    public GameObject playerObject;
    public GameObject skeletonWarriorObject;
    public Animator skeletonWarriorAnimator;
    public LayerMask playerMask;

    public bool isBlocking=false;
    public bool lookingAtPlayer = false;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        skeletonWarriorAgent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");
        skeletonWarriorObject = this.gameObject;
        skeletonWarriorAnimator=skeletonWarriorObject.GetComponent<Animator>();
        playerMask = LayerMask.GetMask("Player");

        skeletonWarriorAgent.speed = stats.movementSpeed;
      
        FSM = new SkeletonWarriorIdle(this);
    }

    void Update()
    {
        FSM = FSM.Process();

        RaycastHit hit;
        if (Physics.Raycast(skeletonWarriorObject.transform.position,transform.TransformDirection(Vector3.forward),out hit,5,playerMask))
        {
            lookingAtPlayer = true;
            Debug.Log("Ve al jugador");
        }
        else
        {
            lookingAtPlayer = false;
        }
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
