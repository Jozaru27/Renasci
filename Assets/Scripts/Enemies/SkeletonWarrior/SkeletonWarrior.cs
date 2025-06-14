using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour, IDamageable
{
    public NavMeshAgent skeletonWarriorAgent;
    public Renderer[] rends;
    public Collider attackTrigger;
    SkeletonWarriorStates FSM;
    Rigidbody rb;

    public EnemyStats stats;
    public GameObject playerObject;
    public GameObject skeletonWarriorObject;
    public Animator skeletonWarriorAnimator;
    public LayerMask playerMask;
    public Quaternion blockTarget = Quaternion.identity;

    public float angularVelocityOnBlock;
    public bool isBlocking=false;
    public bool lookingAtPlayer = false;
    public bool startBlock = false;
    public bool warriorAttackFinish = false;
    public bool isDamageable = false;
    public bool dead;
    public bool goToIdle;
    public bool damaged;
    public bool playerDirectionTaken;
    public bool frozen;

    float distanceToPlayer;
    bool enteredCombat = false;
    bool leftCombat = true;

    public AudioSource audioSource;
    public AudioClip SkeletonWarriorTakeDamage;
    public AudioClip SkeletonWarriorDeath;
    public AudioClip SkeletonWarriorDraw;
    public AudioClip SkeletonWarriorAttack;
    public AudioClip SkeletonWarriorBlock;

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

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        FSM = FSM.Process();

        distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);

        NavMeshPath path = new NavMeshPath();
        bool pathExists = false;
        
        if (skeletonWarriorAgent.CalculatePath(playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
            pathExists = true;

        //if (distanceToPlayer <= stats.detectionDistance && pathExists)
        //    AmbientMusicManager.Instance.EnterCombatMode();
        //else
        //    AmbientMusicManager.Instance.ExitCombatMode();

        if (distanceToPlayer <= stats.detectionDistance && pathExists && !enteredCombat)
        {
            AmbientMusicManager.Instance.EnterCombatMode();
            enteredCombat = true;
            leftCombat = false;
        }
        if ((distanceToPlayer > stats.detectionDistance || !pathExists) && !leftCombat)
        {
            AmbientMusicManager.Instance.ExitCombatMode();
            enteredCombat = false;
            leftCombat = true;
        }

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
        }
    }

    public void TakeDamage(float amount, bool stateDamage)
    {
        float pushedForce = stats.pushedForce;

        if(isBlocking == false && !stateDamage){
            stats.life += amount;
            StartCoroutine(ChangingColor());
            damaged = true;
            PlayTakeDamageSound();
        }
        else
        {
            pushedForce *= 0.5f;
        }
        
        if (stateDamage)
        {
            stats.life += amount;
            StartCoroutine(ChangingColor());
        }

        if (stats.life <= 0)
        {
            PlayDeathSound();
            stats.life = 0;
            AmbientMusicManager.Instance.ExitCombatMode();
            GetComponent<SkeletonWarriorAnimation>().Death();
            GetComponent<CapsuleCollider>().enabled = false;
            UIManager.Instance.ChangeEnemyCount();
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            dead = true;
            skeletonWarriorAgent.isStopped = true;
            GetComponent<LinkWithRoom>().RemoveFromRoomList();
        }

        GetComponent<SkeletonWarriorAnimation>().Hit();
        //GetComponent<SkeletonWarriorAnimation>().Idle();

        if (!stateDamage)
        {
            Vector3 pushDirection = transform.position - playerObject.transform.position;
            pushDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
            rb.AddForce(pushDirection.normalized * pushedForce, ForceMode.VelocityChange);
        }

        if (isBlocking == true)
        {
            PlayBlockSound();
        }

        //damaged = true;////
    }

    IEnumerator ChangingColor()
    {
        foreach (Renderer rend in rends)
        {
            rend.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.125f);

        foreach (Renderer rend in rends)
        {
            rend.material.color = Color.white;
        }
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
        if (distanceToPlayer >= 3)
        {
            StartCoroutine(FinishingAttack());
        }  
    }

    IEnumerator FinishingAttack()
    {
        //yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(0);
        warriorAttackFinish = true;
        GetComponent<SkeletonWarriorAnimation>().Idle();
    }

    public void AttackToBlock()
    {
        if (distanceToPlayer < 3)
        {
            StartCoroutine(AttackingToBlock());
        }
    }

    IEnumerator AttackingToBlock()
    {
        //yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(0f);
        startBlock = true;
    }

    //public void EnableAmbient(){
    //    AmbientSoundManager.Instance.enableCombatMusic = false;
    //}

    public void PlayTakeDamageSound()
    {
        if (SkeletonWarriorTakeDamage != null)
            audioSource.PlayOneShot(SkeletonWarriorTakeDamage, 1f);
    }

    public void PlayDeathSound()
    {
        if (SkeletonWarriorDeath != null)
            audioSource.PlayOneShot(SkeletonWarriorDeath, 3f);
    }

    public void PlayDrawSound()
    {
        if (SkeletonWarriorBlock != null)
            audioSource.PlayOneShot(SkeletonWarriorDraw, 1f);
    }

    public void PlayAttackSound()
    {
        if (SkeletonWarriorAttack != null)
            audioSource.PlayOneShot(SkeletonWarriorAttack, 1f);
    }

    public void PlayBlockSound()
    {
        if (SkeletonWarriorBlock != null)
            audioSource.PlayOneShot(SkeletonWarriorBlock, 1f);
    }
}
