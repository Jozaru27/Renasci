using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMage : MonoBehaviour, IDamageable
{
    public NavMeshAgent skeletonMageAgent;
    public Renderer rend1, rend2;
    SkeletonMageStates FSM;
    Rigidbody rb;

    public EnemyStats stats;
    public GameObject playerObject;
    public GameObject skeletonMageObject;
    public Animator skeletonMageAnimator;
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

    public bool isRepositioning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<EnemyStats>();
        skeletonMageAgent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");
        skeletonMageObject = this.gameObject;
        skeletonMageAnimator = skeletonMageObject.GetComponent<Animator>();
        playerMask = LayerMask.GetMask("Player");

        skeletonMageAgent.speed = stats.movementSpeed;

        FSM = new SkeletonMageIdle(this);
    }

    void Update()
    {
        FSM = FSM.Process();

        RaycastHit hit;
        if (Physics.Raycast(skeletonMageObject.transform.position, transform.TransformDirection(Vector3.forward), out hit, 5, playerMask))
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

        if (stats.life <= 0)
        {
            stats.life = 0;
            GetComponent<SkeletonMageAnimation>().Death();
            GetComponent<CapsuleCollider>().enabled = false;
            UIManager.Instance.ChangeEnemyCount();
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            dead = true;
            skeletonMageAgent.isStopped = true;
        }

        if (!stateDamage)
        {
            Vector3 pushDirection = transform.position - playerObject.transform.position;
            pushDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
            rb.AddForce(pushDirection.normalized * pushedForce, ForceMode.VelocityChange);
        }
    }

    IEnumerator ChangingColor()
    {
        rend1.material.color = Color.red;
        rend2.material.color = Color.red;

        yield return new WaitForSeconds(0.125f);

        rend1.material.color = Color.white;
        rend2.material.color = Color.white;
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
        GetComponent<SkeletonMageAnimation>().Idle();
    }

    // Crea una flecha que sale dispara en dirección al jugador y le daña
    public void AttackPlayer()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Rigidbody ArrowRb = arrow.GetComponent<Rigidbody>();
        arrow.GetComponent<Arrow>().damage = stats.mainDamage;
        arrow.GetComponent<Arrow>().pushForce = stats.pushForce;

        if (ArrowRb != null)
        {
            Vector3 adjustedPlayerPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + 1f, playerObject.transform.position.z);

            Vector3 direction = (adjustedPlayerPosition - firePoint.position).normalized;

            Quaternion rotation = Quaternion.LookRotation(direction);
            arrow.transform.rotation = rotation * Quaternion.Euler(90f, 0f, 0f);

            ArrowRb.AddForce(direction * 15f, ForceMode.Impulse);
        }
    }

    // Hace que el mago se teletransporte cuando el jugador se acerca demasiado. Busca un sitio válido en la sala, si lo encuentra se teletransporta
    public IEnumerator Teleporting(float duration)
    {
        damaged = false;

        yield return new WaitForSeconds(duration);

        Vector3 dirToPlayer = (skeletonMageObject.transform.position - playerObject.transform.position).normalized;
        Vector3 targetPosition = playerObject.transform.position + (dirToPlayer * stats.detectionDistance);
        Vector3 firstVector = dirToPlayer;

        bool exitLoop = false;

        NavMeshPath path = new NavMeshPath();

        while (!skeletonMageAgent.CalculatePath(targetPosition, path) && !exitLoop)
        {
            dirToPlayer = (Quaternion.AngleAxis(10f, Vector3.up) * dirToPlayer).normalized;

            targetPosition = playerObject.transform.position + (dirToPlayer * stats.detectionDistance);

            if (dirToPlayer == firstVector && path.status == NavMeshPathStatus.PathComplete)
                exitLoop = true;
        }

        //if (!(skeletonMageAgent.CalculatePath(targetPosition, path) && path.status == NavMeshPathStatus.PathComplete))
        //{
        //    FSM = new SkeletonMageAttack(this);
        //    isRepositioning = false;
        //    yield break;
        //}

        skeletonMageAgent.isStopped = false;
        //skeletonMageAgent.SetDestination(targetPosition);

        if (!dead)
            transform.position = targetPosition;
        //skeletonMageObject.GetComponent<SkeletonMageAnimation>().Run();

        while (skeletonMageAgent.pathPending || skeletonMageAgent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        float distanceToPlayer = Vector3.Distance(skeletonMageObject.transform.position, playerObject.transform.position);

        if (distanceToPlayer >= 5f && distanceToPlayer <= stats.detectionDistance)
            FSM = new SkeletonMageAttack(this);
        else
            yield return new WaitForSeconds(2f);

        isRepositioning = false;
    }
}
