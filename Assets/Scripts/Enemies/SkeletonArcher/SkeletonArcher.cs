using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcher : MonoBehaviour, IDamageable
{
    public NavMeshAgent skeletonArcherAgent;
    public Renderer rend1, rend2;
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

    public GameObject arrowPrefab;
    public Transform firePoint;

    float distanceToPLayer;

    public bool isRepositioning = false;

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

    public void TakeDamage(float amount, bool stateDamage)
    {
        float pushedForce = stats.pushedForce;

        stats.life += amount;
        StartCoroutine(ChangingColor());
        damaged = true;

        if (stats.life <= 0)
        {
            stats.life = 0;
            GetComponent<SkeletonArcherAnimation>().Death();
            GetComponent<CapsuleCollider>().enabled = false;
            UIManager.Instance.ChangeEnemyCount();
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            dead = true;
            skeletonArcherAgent.isStopped = true;
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
        GetComponent<SkeletonArcherAnimation>().Idle();
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

    // Hace que el arquero se reposicione cuando el jugador se acerca demasiado. Busca un camino válido, si lo encuentra huye, si no, ataca. Si se reposiciona, valora la distancia para decidir su próximo estado.
    public IEnumerator WaitAndReposition()
    {
        yield return new WaitForSeconds(0.5f);

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

        while (skeletonArcherAgent.pathPending || skeletonArcherAgent.remainingDistance > 0.5f)
        {
            yield return null;
        }

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
        }

        isRepositioning = false; 
    }

}