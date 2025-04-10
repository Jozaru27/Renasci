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
    public LayerMask secondAttackMask;

    //public float angularVelocityOnBlock;
    //public bool isBlocking=false;
    public bool lookingAtPlayer = false;
    //public bool startBlock = false;
    public bool mageAttackFinish = false;
    public bool isDamageable = false;
    public bool attacking = false;
    public bool secondAttack = false;
    public bool dead;
    public bool goToIdle;
    public bool damaged;
    public bool playerDirectionTaken;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform secondFirePoint;

    public bool teleporting = false; // JOSE: AÑADIDO BOOL DE HAS TELEPORTED

    float distanceToPlayer;
    bool inCombat;

    [SerializeField] GameObject cylinder; //PLACEHOLDER
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

        distanceToPlayer = Vector3.Distance(skeletonMageObject.transform.position, playerObject.transform.position);

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

        GetComponent<SkeletonMageAnimation>().Hit();

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
        mageAttackFinish = true;
        goToIdle = true;
        GetComponent<SkeletonMageAnimation>().Idle();
    }

    // Crea una flecha que sale dispara en direcci�n al jugador y le da�a
    public void AttackPlayer()
    {
        GameObject magicBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody bulletRb = magicBullet.GetComponent<Rigidbody>();
        magicBullet.GetComponent<MagicBullet>().damage = stats.mainDamage;
        magicBullet.GetComponent<MagicBullet>().pushForce = stats.pushForce;

        //if (bulletRb != null)
        //{
        //    Vector3 adjustedPlayerPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + 1f, playerObject.transform.position.z);

        //    Vector3 direction = (adjustedPlayerPosition - firePoint.position).normalized;

        //    Quaternion rotation = Quaternion.LookRotation(direction);
        //    magicBullet.transform.rotation = rotation * Quaternion.Euler(90f, 0f, 0f);

        //    bulletRb.AddForce(direction * 15f, ForceMode.Impulse);
        //}

        attacking = false;
    }

    public IEnumerator MakingSecondAttack()
    {
        secondAttack = true;

        yield return new WaitForSeconds(0.5f);

        float timer = 0;
        cylinder.SetActive(true);

        GetComponent<SkeletonMageAnimation>().SecondAttack();

        while (timer < 3.5f && !teleporting)
        {
            Vector3 playerDirection = playerObject.transform.position - skeletonMageObject.transform.position;
            Quaternion playerRotation = Quaternion.LookRotation(playerDirection.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, 20 * Time.deltaTime);

            Ray attackRay = new Ray(secondFirePoint.transform.position, transform.forward);
            Debug.DrawRay(secondFirePoint.transform.position, transform.forward * 100, Color.red);

            if (Physics.Raycast(attackRay, out RaycastHit hit, Mathf.Infinity, secondAttackMask))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                    hit.collider.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-stats.secondaryDamage, hit.point, stats.pushForce);
            }

            timer += Time.deltaTime;

            yield return null;
        }

        secondAttack = false;
        attacking = false;
        cylinder.SetActive(false);

        mageAttackFinish = true;
        goToIdle = true;
        GetComponent<SkeletonMageAnimation>().Idle();
    }

    // Hace que el mago se teletransporte cuando el jugador se acerca demasiado. Busca un sitio v�lido en la sala, si lo encuentra se teletransporta
    // public IEnumerator Teleporting(float duration)
    // {
    //     Debug.Log("A");

    //     damaged = false;

    //     yield return new WaitForSeconds(duration);

    //     Vector3 dirToPlayer = (skeletonMageObject.transform.position - playerObject.transform.position).normalized;
    //     Vector3 targetPosition = playerObject.transform.position - (dirToPlayer * stats.detectionDistance);
    //     Vector3 firstVector = dirToPlayer;

    //     bool exitLoop = false;
    //     bool successfullLoop = false;

    //     NavMeshPath path = new NavMeshPath();

    //     int iterations = 0;

    //     NavMeshHit hit;

    //     //while (!skeletonMageAgent.CalculatePath(targetPosition, path) && !exitLoop)
    //     //{
    //     //    Debug.Log("B");
    //     //    //dirToPlayer = (Quaternion.AngleAxis(10f, Vector3.up) * dirToPlayer).normalized;
    //     //    dirToPlayer = (Quaternion.AngleAxis(10f, Vector3.up) * dirToPlayer).normalized;

    //     //    targetPosition = playerObject.transform.position - (dirToPlayer * stats.detectionDistance);

    //     //    iterations++;

    //     //    Debug.Log(path.status);

    //     //    if (NavMesh.SamplePosition(playerObject.transform.position, out hit, 10f, NavMesh.AllAreas))
    //     //    {
    //     //        Debug.Log("JOZARU");
    //     //    }
    //     //    else
    //     //        Debug.Log("MARICON");

    //     //    if (path.status == NavMeshPathStatus.PathComplete /*&& */)
    //     //    {
    //     //        Debug.Log("C");
    //     //        exitLoop = true;
    //     //        successfullLoop = true;
    //     //    }
    //     //    else if (iterations >= 36 || dirToPlayer == firstVector)
    //     //    {
    //     //        Debug.Log("D");
    //     //        exitLoop = true;
    //     //    }
    //     //}

    //     //if (!(skeletonMageAgent.CalculatePath(targetPosition, path) && path.status == NavMeshPathStatus.PathComplete))
    //     //{
    //     //    FSM = new SkeletonMageAttack(this);
    //     //    isRepositioning = false;
    //     //    yield break;
    //     //}

    //     skeletonMageAgent.isStopped = false;
    //     //skeletonMageAgent.SetDestination(targetPosition);

    //     if (!dead && successfullLoop)
    //     {
    //         Debug.Log("E");
    //         transform.position = targetPosition;
    //     }
    //     //skeletonMageObject.GetComponent<SkeletonMageAnimation>().Run();

    //     while (skeletonMageAgent.pathPending || skeletonMageAgent.remainingDistance > 0.5f)
    //     {
    //         yield return null;
    //     }

    //     float distanceToPlayer = Vector3.Distance(skeletonMageObject.transform.position, playerObject.transform.position);

    //     if (distanceToPlayer >= 5f && distanceToPlayer <= stats.detectionDistance)
    //         FSM = new SkeletonMageAttack(this);
    //     else
    //         yield return new WaitForSeconds(2f);

    //     isRepositioning = false;
    // }

    //JOSE: ESPERA 1F INDICADO EN FOLLOW. EVITA TELETRANSPORTACIONES MULTIPLES. SEGÚN LA POSICIÓN DEL JUGADOR, BUSCA UNA DISTANCIA RANDOM ENTRE 5F Y 10F DE DISTANCIA DEL MISMO
    //JOSE: TRAS MARCAR UNA DISTANCIA, CALCULA EL PATH. SI PUEDE LLEGAR (ES DECIR, EL NAVMESH ESTÁ CONECTADO, HACE UN WARP [TELETRANSPORTE INSTANTÁNEO]).
    public IEnumerator Teleporting(float duration)
    {
        GetComponent<SkeletonMageAnimation>().Teleport();

        damaged = false;

        yield return new WaitForSeconds(duration);

        //if (hasTeleported) yield break;

        Vector3 dirToPlayer = (skeletonMageObject.transform.position - playerObject.transform.position).normalized;
        float minDistance = 5f;
        float maxDistance = 10f;
        Vector3 targetPosition;

        NavMeshPath path = new NavMeshPath();

        float randomDistance = Random.Range(minDistance, maxDistance);
        //Vector3 rotatedDir = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * dirToPlayer;
        Vector3 rotatedDir = Quaternion.AngleAxis(Random.Range(0, 360f), Vector3.up) * dirToPlayer;
        targetPosition = playerObject.transform.position - rotatedDir * randomDistance;

        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            if (skeletonMageAgent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
                skeletonMageAgent.Warp(hit.position);
        }

        float distanceToPlayerFinal = Vector3.Distance(skeletonMageObject.transform.position, playerObject.transform.position);

        transform.rotation = Quaternion.LookRotation(dirToPlayer * -1);

        //if (distanceToPlayerFinal <= stats.detectionDistance)
        //    FSM = new SkeletonMageAttack(this);
        //else
        //    yield return new WaitForSeconds(0.1f);

        teleporting = false;
    }

    // public IEnumerator Teleporting(float duration)
    // {
    //     damaged = false;
    //     yield return new WaitForSeconds(duration);

    //     Vector3 dirToPlayer = (skeletonMageObject.transform.position - playerObject.transform.position).normalized;
    //     float radius = stats.detectionDistance;
    //     Vector3 targetPosition;
    //     bool foundValidSpot = false;

    //     NavMeshPath path = new NavMeshPath();
    //     int maxAttempts = 36;
    //     int attempts = 0;

    //     while (attempts < maxAttempts)
    //     {
    //         float angle = attempts * 10f;
    //         Vector3 rotatedDir = Quaternion.Euler(0f, angle, 0f) * dirToPlayer;
    //         targetPosition = playerObject.transform.position - rotatedDir.normalized * radius;

    //         if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1f, NavMesh.AllAreas))
    //         {
    //             if (skeletonMageAgent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
    //             {
    //                 skeletonMageAgent.Warp(hit.position);
    //                 foundValidSpot = true;
    //                 break;
    //             }
    //         }

    //         attempts++;
    //     }

    //     if (!foundValidSpot)
    //         yield return new WaitForSeconds(2f);

    //     float distanceToPlayer = Vector3.Distance(skeletonMageObject.transform.position, playerObject.transform.position);

    //     if (distanceToPlayer <= stats.detectionDistance)
    //         FSM = new SkeletonMageAttack(this);
    //     else
    //         yield return new WaitForSeconds(2f);

    //     isRepositioning = false;
    // }

}