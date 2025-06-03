using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcher : MonoBehaviour, IDamageable
{
    public NavMeshAgent skeletonArcherAgent;
    public Renderer[] rends;
    SkeletonArcherStates FSM;
    Rigidbody rb;

    public EnemyStats stats;
    public GameObject playerObject;
    public GameObject skeletonArcherObject;
    public Animator skeletonArcherAnimator;
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
    public bool frozen;

    public GameObject arrowPrefab;
    public Transform firePoint;

    float distanceToPlayer;
    bool enteredCombat = false;
    bool leftCombat = true;

    public bool isRepositioning = false;

    public AudioSource audioSource;

    public AudioClip SkeletonArcherTakeDamage;
    public AudioClip SkeletonArcherDeath;
    public AudioClip SkeletonArcherAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<EnemyStats>();
        skeletonArcherAgent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");
        skeletonArcherObject = this.gameObject;
        skeletonArcherAnimator = skeletonArcherObject.GetComponent<Animator>();
        playerMask = LayerMask.GetMask("Player");

        skeletonArcherAgent.speed = stats.movementSpeed;
      
        FSM = new SkeletonArcherIdle(this);
    }

    void Update()
    {
        FSM = FSM.Process();

        distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);

        NavMeshPath path = new NavMeshPath();
        bool pathExists = false;

        if (skeletonArcherAgent.CalculatePath(playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
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

        if (skeletonArcherAgent.CalculatePath(playerObject.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
            pathExists = true;

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

    public void TakeDamage(float amount, bool stateDamage)
    {
        float pushedForce = stats.pushedForce;

        stats.life += amount;
        StartCoroutine(ChangingColor());
        damaged = true;
        PlayTakeDamageSound();

        if (stats.life <= 0)
        {
            stats.life = 0;
            AmbientMusicManager.Instance.ExitCombatMode();
            GetComponent<SkeletonArcherAnimation>().Death();
            GetComponent<CapsuleCollider>().enabled = false;
            UIManager.Instance.ChangeEnemyCount();
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            dead = true;
            skeletonArcherAgent.isStopped = true;
            GetComponent<LinkWithRoom>().RemoveFromRoomList();
        }

        //GetComponent<SkeletonArcherAnimation>().Hit();
        //GetComponent<SkeletonArcherAnimation>().Idle();

        if (!stateDamage)
        {
            Vector3 pushDirection = transform.position - playerObject.transform.position;
            pushDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
            rb.AddForce(pushDirection.normalized * pushedForce, ForceMode.VelocityChange);
        }

        damaged = true;
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

    public void FinishAttack()
    {
        archerAttackFinish = true;
        goToIdle = true;
        GetComponent<SkeletonArcherAnimation>().Idle();
    }

    // Crea una flecha que sale dispara en dirección al jugador y le daña
    public void AttackPlayer()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Rigidbody ArrowRb = arrow.GetComponent<Rigidbody>();
        arrow.GetComponent<Arrow>().damage = stats.mainDamage;
        arrow.GetComponent<Arrow>().pushForce = stats.pushForce;
        arrow.GetComponent<Arrow>().SetShooter(this.gameObject);

        if (ArrowRb != null)
        {
            Vector3 adjustedPlayerPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + 1f, playerObject.transform.position.z);

            Vector3 direction = (adjustedPlayerPosition - firePoint.position).normalized;

            Quaternion rotation = Quaternion.LookRotation(direction);
            arrow.transform.rotation = rotation * Quaternion.Euler(90f, 0f, 0f);

            ArrowRb.AddForce(direction * 15f, ForceMode.Impulse);
        }
    }

    // Hace que el arquero se reposicione cuando el jugador se acerca demasiado. Busca un camino válido, si lo encuentra huye, si no, ataca. Si se reposiciona, valora la distancia para decidir su próximo estado.
    
    // public IEnumerator WaitAndReposition()
    // {
    //     yield return new WaitForSeconds(0.5f);

    //     if (dead)
    //     {
    //         isRepositioning = false;
    //         yield break;
    //     }

    //     Vector3 dirToPlayer = (skeletonArcherObject.transform.position - playerObject.transform.position).normalized;
    //     Vector3 targetPosition = playerObject.transform.position + dirToPlayer * 6f;

    //     NavMeshPath path = new NavMeshPath();

    //     if (!(skeletonArcherAgent.CalculatePath(targetPosition, path) && path.status == NavMeshPathStatus.PathComplete))
    //     {
    //         FSM = new SkeletonArcherAttack(this);
    //         isRepositioning = false;
    //         yield break; 
    //     }

    //     skeletonArcherAgent.isStopped = false;
    //     skeletonArcherAgent.SetDestination(targetPosition);
    //     skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Run();

    //     while (skeletonArcherAgent.pathPending || skeletonArcherAgent.remainingDistance > 0.5f)
    //     {
    //         yield return null;
    //         skeletonArcherAgent.ResetPath();
    //         skeletonArcherAgent.isStopped = true;
    //     }

    //     float distanceToPlayer = Vector3.Distance(skeletonArcherObject.transform.position, playerObject.transform.position);
    //     if (distanceToPlayer >= 5f && distanceToPlayer <= 7f)
    //     {
    //         FSM = new SkeletonArcherAttack(this);
    //     }
    //     else if (distanceToPlayer > 7f)
    //     {
    //         FSM = new SkeletonArcherFollow(this);
    //     }
    //     else
    //     {
    //         yield return new WaitForSeconds(2f);
    //     }

    //     isRepositioning = false; 
    // }

    public IEnumerator WaitAndReposition()
    {
        yield return new WaitForSeconds(0.5f);

        if (dead)
        {
            isRepositioning = false;
            yield break;
        }

        Vector3 dirToPlayer = (skeletonArcherObject.transform.position - playerObject.transform.position).normalized;
        Vector3 targetPosition = playerObject.transform.position + dirToPlayer * 6f;

        NavMeshPath path = new NavMeshPath();

        if (!(skeletonArcherAgent.CalculatePath(targetPosition, path) && path.status == NavMeshPathStatus.PathComplete))
        {
            FSM = new SkeletonArcherAttack(this);
            isRepositioning = false;
            yield break;
        }

        skeletonArcherAgent.isStopped = false;
        skeletonArcherAgent.SetDestination(targetPosition);
        skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Run();

        float timeout = 3.5f;
        float elapsed = 0f;

        while ((skeletonArcherAgent.pathPending || skeletonArcherAgent.remainingDistance > 0.5f) && elapsed < timeout)
        {
            if (skeletonArcherAgent.velocity.sqrMagnitude < 0.01f && !skeletonArcherAgent.pathPending)
            {
                Debug.LogWarning("Archer atascado durante reposicionamiento.");
                break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        skeletonArcherAgent.ResetPath();
        skeletonArcherAgent.isStopped = true;
        skeletonArcherObject.GetComponent<SkeletonArcherAnimation>().Idle();

        float distanceToPlayer = Vector3.Distance(skeletonArcherObject.transform.position, playerObject.transform.position);
        if (distanceToPlayer >= 5f && distanceToPlayer <= 7f)
        {
            FSM = new SkeletonArcherAttack(this);
        }
        else if (distanceToPlayer > 7f)
        {
            FSM = new SkeletonArcherFollow(this);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            FSM = new SkeletonArcherAttack(this);
        }

        isRepositioning = false;
    }

    public void PlayTakeDamageSound()
    {
        if (SkeletonArcherTakeDamage != null)
            audioSource.PlayOneShot(SkeletonArcherTakeDamage, 1f);
    }

    public void PlayDeathSound()
    {
        if (SkeletonArcherDeath != null)
            audioSource.PlayOneShot(SkeletonArcherDeath, 3f);
    }

    public void PlayAttackSound()
    {
        if (SkeletonArcherAttack != null)
            audioSource.PlayOneShot(SkeletonArcherAttack, 1f);
    }
}