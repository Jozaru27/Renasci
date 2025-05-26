using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMage : MonoBehaviour, IDamageable
{
    [Header("Particles")]
    [SerializeField] GameObject initialTpParticles;
    [SerializeField] GameObject finalTpParticles;
    [SerializeField] GameObject rayCollisionParticles;
    [SerializeField] GameObject rayRecharge;

    [Header("Renderers")]
    [SerializeField] Renderer[] rends;
    [SerializeField] LineRenderer rayRend;

    [Header("EnemyAttack")]
    [SerializeField] LayerMask playerMask;

    [Header("Bullet Attack Management")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletFirePoint;

    [Header("Ray Attack Management")]
    [SerializeField] Transform rayFirePoint;
    [SerializeField] GameObject magicRay;
    [SerializeField] LayerMask rayAttackMask;

    public bool teleporting;
    public bool attacking;
    public bool frozen;
    public bool damaged;
    public bool dead;
    public bool goToIdle;
    public bool lookingAtPlayer;

    [HideInInspector] public GameObject playerObj;
    [HideInInspector] public EnemyStats stats;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;

    bool usingRay;
    Rigidbody rb;

    SkeletonMageStates FSM;
    
    private void Start()
    {
        playerObj = GameObject.Find("Player");
        stats = GetComponent<EnemyStats>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent.speed = stats.movementSpeed;

        FSM = new SkeletonMageIdle(this);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerObj.transform.position);

        NavMeshPath path = new NavMeshPath();
        bool pathExists = false;

        if (agent.CalculatePath(playerObj.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
            pathExists = true;

        if (distanceToPlayer <= stats.detectionDistance && pathExists)
            AmbientMusicManager.Instance.EnterCombatMode();
        else
            AmbientMusicManager.Instance.ExitCombatMode();

        Ray visionRay = new Ray(transform.position, transform.forward);

        if (usingRay && Physics.Raycast(visionRay, out RaycastHit magicRayHit, Mathf.Infinity, rayAttackMask))
        {
            float rayLenght = Vector3.Distance(magicRayHit.point, rayRend.gameObject.transform.position);
            Vector3 finalPoint = new Vector3(0, 0, rayLenght);
            rayRend.SetPosition(1, finalPoint);
            rayCollisionParticles.transform.position = magicRayHit.point;
            rayCollisionParticles.transform.position = new Vector3(rayCollisionParticles.transform.position.x, rayRend.gameObject.transform.position.y, rayCollisionParticles.transform.position.z);
        }

        if (Physics.Raycast(visionRay, out RaycastHit visionRayHit, Mathf.Infinity, playerMask))
            lookingAtPlayer = true;
        else
            lookingAtPlayer = false;

        FSM = FSM.Process();
    }

//    if (!inCombat)
//        {
//            AmbientMusicManager.Instance.EnterCombatMode();
//            inCombat = true;
//        }

    public void BulletAttack()
    {
        GameObject magicBullet = Instantiate(bulletPrefab, bulletFirePoint.position, Quaternion.identity);
        magicBullet.GetComponent<MagicBullet>().damage = stats.mainDamage;
        magicBullet.GetComponent<MagicBullet>().pushForce = stats.pushForce;
    }

    public void FinishAttack()
    {
        //mageAttackFinish = true;

        goToIdle = true;
        attacking = false;
    }

    public void InitiateRay()
    {
        rayRecharge.SetActive(true);
        rayRecharge.GetComponent<ParticleSystem>().Play();
    }

    public IEnumerator RayAttack()
    {
        float timer = 0;
        magicRay.SetActive(true);
        rayRecharge.SetActive(false);

        while (timer < 3.5f)
        {
            yield return new WaitUntil(() => !frozen);

            usingRay = true;

            Vector3 playerDirection = playerObj.transform.position - transform.position;
            Quaternion playerRotation = Quaternion.LookRotation(playerDirection.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, 20 * Time.deltaTime);

            Ray attackRay = new Ray(rayFirePoint.transform.position, transform.forward);

            if (Physics.Raycast(attackRay, out RaycastHit hit, Mathf.Infinity, rayAttackMask))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                    hit.collider.gameObject.GetComponent<PlayerHealth>().ChangeHealthAmount(-stats.secondaryDamage, hit.point, stats.pushForce);
            }

            timer += Time.deltaTime;

            yield return null;
        }

        usingRay = false;
        attacking = false;
        magicRay.SetActive(false);
    }

    public void UseTeleport()
    {
        initialTpParticles.GetComponent<ParticleSystem>().Play();

        float minDistance = 5f;
        float maxDistance = 10f;
        float randomDistance = Random.Range(minDistance, maxDistance);

        Vector3 targetPosition;
        Vector3 directionToPlayer = (transform.position - playerObj.transform.position).normalized;
        Vector3 rotatedDir = Quaternion.AngleAxis(Random.Range(0, 360f), Vector3.up) * directionToPlayer;

        targetPosition = playerObj.transform.position - rotatedDir * randomDistance;

        NavMeshPath path = new NavMeshPath();

        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            if (agent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
                agent.Warp(hit.position);
        }

        float distanceToPlayerFinal = Vector3.Distance(transform.position, playerObj.transform.position);

        transform.rotation = Quaternion.LookRotation(directionToPlayer * -1);

        finalTpParticles.GetComponent<ParticleSystem>().Play();

        Debug.Log("SE LE QUITA EL TELEPORT");

        teleporting = false;
        goToIdle = true;
    }

    public void TakeDamage(float amount, bool stateDamage)
    {
        float pushedForce = stats.pushedForce;

        stats.life += amount;
        StopAllCoroutines();
        usingRay = false;
        attacking = false;
        magicRay.SetActive(false);
        StartCoroutine(ChangingColor());
        damaged = true;

        if (stats.life <= 0)
        {
            stats.life = 0;
            AmbientMusicManager.Instance.ExitCombatMode();
            GetComponent<SkeletonMageAnimation>().Death();
            GetComponent<CapsuleCollider>().enabled = false;
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            dead = true;
            agent.isStopped = true;
            GetComponent<LinkWithRoom>().RemoveFromRoomList();
        }

        if (!teleporting)
            GetComponent<SkeletonMageAnimation>().Hit();

        if (!stateDamage)
        {
            Vector3 pushDirection = transform.position - playerObj.transform.position;
            pushDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
            rb.AddForce(pushDirection.normalized * pushedForce, ForceMode.VelocityChange);
        }
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

    //public void Teleporting()
    //{
    //    //if (hasTeleported) yield break;

    //    //if (distanceToPlayerFinal <= stats.detectionDistance)
    //    //    FSM = new SkeletonMageAttack(this);
    //    //else
    //    //    yield return new WaitForSeconds(0.1f);

    //    goToIdle = true;
    //}

    //public IEnumerator MakingSecondAttack()
    //{
    //    while (timer < 3.5f && !teleporting)
    //    {

    //    }
    //    secondAttack = false;

    //    if (!teleporting)
    //        goToIdle = true;
    //    //GetComponent<SkeletonMageAnimation>().Idle();
    //}

    //public void UseTeleportAnim()
    //{
    //    GetComponent<SkeletonMageAnimation>().Teleport();
    //    //StartCoroutine(Teleporting());
    //    Debug.Log("INICIALIZANDO");
    //    damaged = false;
    //}



























    //public LayerMask playerMask;

    //public bool lookingAtPlayer = false;
    //public bool mageAttackFinish = false;
    //public bool isDamageable = false;
    //public bool secondAttack = false;
    //public bool goToIdle;
    //public bool playerDirectionTaken;

    //public bool teleporting = false; // JOSE: AÑADIDO BOOL DE HAS TELEPORTED

    //float distanceToPlayer;
    //bool inCombat;

    //[SerializeField] LayerMask collisionLayer;

    //[SerializeField] GameObject magicRay;
    //[SerializeField] Collider rayCollision;
}