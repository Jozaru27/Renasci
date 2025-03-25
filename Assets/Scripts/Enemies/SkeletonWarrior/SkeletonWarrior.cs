using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour, IDamageable
{
    public NavMeshAgent skeletonWarriorAgent;
    public Renderer rend1, rend2, rend3;
    public Collider attackTrigger;
    SkeletonWarriorStates FSM;
    Rigidbody rb;

    public EnemyStats stats;
    public GameObject playerObject;
    public GameObject skeletonWarriorObject;
    public Animator skeletonWarriorAnimator;
    public LayerMask playerMask;

    public bool isBlocking=false;
    public bool lookingAtPlayer = false;
    public bool startBlock = false;
    public bool warriorAttackFinish = false;
    public bool isDamageable = false;

    float distanceToPLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        distanceToPLayer = Vector3.Distance(skeletonWarriorObject.transform.position, playerObject.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(skeletonWarriorObject.transform.position,transform.TransformDirection(Vector3.forward),out hit,5,playerMask))
        {
            lookingAtPlayer = true;
        }
        else
        {
            lookingAtPlayer = false;
        }
    }

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player") && isBlocking==false){
            other.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-stats.mainDamage, skeletonWarriorObject.transform.position, stats.pushForce);
            attackTrigger.enabled = false;
        }else if (other.gameObject.CompareTag("Player") && isBlocking == true)
        {
            isDamageable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isBlocking == true)
        {
            isDamageable = false;
        }
    }

    public void TakeDamage(float amount)
    {
        float pushedForce = stats.pushedForce;

        if(isBlocking==false || isDamageable == true){
            stats.life += amount;
            StartCoroutine(ChangingColor());
        }
        else{
            pushedForce *= 0.5f;
        }

        if (stats.life <= 0)
        {
            stats.life = 0;
            GetComponent<SkeletonWarriorAnimation>().Death();
            GetComponent<CapsuleCollider>().enabled = false;
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
        }

        //GetComponent<SkeletonWarriorAnimation>().Hit();

        Vector3 pushDirection = transform.position - playerObject.transform.position;
        pushDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
        rb.AddForce(pushDirection.normalized * pushedForce, ForceMode.Impulse);
    }

    IEnumerator ChangingColor()
    {
        //Placeholder
        rend1.material.color = Color.red;
        rend2.material.color = Color.red;
        rend3.material.color = Color.red;

        yield return new WaitForSeconds(0.125f);

        rend1.material.color = Color.white;
        rend2.material.color = Color.white;
        rend3.material.color = Color.white;
    }

    public void DestroyThisObject()
    {
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
        Debug.Log("A");
        if (distanceToPLayer >= 3)
        {
            Debug.Log("B");
            StartCoroutine(FinishingAttack());
        }  
    }

    IEnumerator FinishingAttack()
    {
        yield return new WaitForSeconds(1.5f);
        warriorAttackFinish = true;
        Debug.Log("Termina ataque");
    }

    public void AttackToBlock()
    {
        if (distanceToPLayer < 3)
        {
            StartCoroutine(AttackingToBlock());
        }
    }

    IEnumerator AttackingToBlock()
    {
        yield return new WaitForSeconds(1.5f);
        startBlock = true;
        Debug.Log("EmpiezaBloqueo");
    }
}
