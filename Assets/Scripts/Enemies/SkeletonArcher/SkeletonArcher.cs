using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcher : MonoBehaviour, IDamageable
{
    public NavMeshAgent skeletonArcherAgent;
    //public Renderer rend1, rend2, rend3;
    public Collider attackTrigger;
    SkeletonArcherStates FSM;
    Rigidbody rb;

    public EnemyStats stats;
    public GameObject playerObject;
    public GameObject skeletonArcherObject;
    //public Animator skeletonArcherAnimator;
    public LayerMask playerMask;

    //public float angularVelocityOnBlock;
    //public bool isBlocking=false;
    public bool lookingAtPlayer = false;
    //public bool startBlock = false;
    public bool archerAttackFinish = false;
    public bool isDamageable = false;
    public bool dead;
    public bool goToIdle;
    public bool damaged;
    public bool playerDirectionTaken;

    public GameObject arrowPrefab;
    public Transform firePoint;

    float distanceToPLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<EnemyStats>();
        skeletonArcherAgent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");
        skeletonArcherObject = this.gameObject;
        //skeletonArcherAnimator=skeletonArcherObject.GetComponent<Animator>();
        playerMask = LayerMask.GetMask("Player");

        skeletonArcherAgent.speed = stats.movementSpeed;
      
        FSM = new SkeletonArcherIdle(this);
    }

    void Update()
    {
        FSM = FSM.Process();

        distanceToPLayer = Vector3.Distance(skeletonArcherObject.transform.position, playerObject.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(skeletonArcherObject.transform.position, transform.TransformDirection(Vector3.forward), out hit, 5, playerMask))
        {
            lookingAtPlayer = true;
        }
        else
        {
            lookingAtPlayer = false;
        }
    }

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-stats.mainDamage, skeletonArcherObject.transform.position, stats.pushForce);
            attackTrigger.enabled = false;
        }
    }

    public void TakeDamage(float amount, bool stateDamage)
    {
        float pushedForce = stats.pushedForce;

        stats.life += amount;
        StartCoroutine(ChangingColor());
        damaged = true;

        if (stats.life <= 0)
        {
            stats.life = 0;
            //GetComponent<SkeletonArcherAnimation>().Death();
            GetComponent<CapsuleCollider>().enabled = false;
            UIManager.Instance.ChangeEnemyCount();
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            dead = true;
        }

        //GetComponent<SkeletonArcherAnimation>().Hit();
        //GetComponent<SkeletonArcherAnimation>().Idle();

        if (!stateDamage)
        {
            Vector3 pushDirection = transform.position - playerObject.transform.position;
            pushDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
            rb.AddForce(pushDirection.normalized * pushedForce, ForceMode.VelocityChange);
        }

        damaged = true;////
    }

    IEnumerator ChangingColor()
    {
        //Placeholder
        //rend1.material.color = Color.red;
        //rend2.material.color = Color.red;
        //rend3.material.color = Color.red;

        yield return new WaitForSeconds(0.125f);

        //rend1.material.color = Color.white;
        //rend2.material.color = Color.white;
        //rend3.material.color = Color.white;
    }

    public void DestroyThisObject()
    {
        LootSpawnManager.Instance.LootProbability(transform.position);
        Destroy(this.gameObject);
    }

    public void EnableAttackTrigger()
    {
        attackTrigger.enabled = true;
    }

    public void DisableAttackTrigger()
    {
        attackTrigger.enabled = false;
    }
    /*
    public void GoingToBlock()
    {
        startBlock = true;
    }
    */

    public void FinishAttack()
    {
        if (distanceToPLayer >= 9)
        {
            StartCoroutine(FinishingAttack());
        }  
    }

    IEnumerator FinishingAttack()
    {
        //yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(0);
        archerAttackFinish = true;
        //GetComponent<SkeletonArcherAnimation>().Idle();
        Debug.Log("Termina ataque");
    }
}
