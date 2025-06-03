using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [Header("Distance Attack")]
    [SerializeField] public float rechargeTime;
    [SerializeField] float recoilForce;
    [SerializeField] GameObject bulletPref;
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject indicatorObj;
    [SerializeField] LayerMask rayMask;

    [Header("Relic Behaviour")]
    [SerializeField] float fireDistance;
    [SerializeField] LayerMask burnableMask;

    [Header("Relic Object")]
    [SerializeField] GameObject iceObj;
    [SerializeField] GameObject windObj;

    [Header("Fire Relic VFX")]
    [SerializeField] GameObject fireWispPrefab;
    [SerializeField] int totalWisps = 4;
    [SerializeField] float spiralDuration = 2f;
    [SerializeField] float spiralOutwardDistance = 3f;
    [SerializeField] float spiralRotations = 2f;

    [Header("Audio")]
    [SerializeField] AudioClip relicRotationClip;
    [SerializeField] AudioClip[] relicClips; 

    int shots;
    public int relicSlot = 0;
    bool shotable = true;
    bool shoting;
    public bool relicUsable = true;
    public bool inRelicCooldown;
    bool usingIndicator;
    public bool attacking;
    Vector2 mousePos;
    Vector3 collidePosition;
    Rigidbody rb;
    PlayerInput input;

    List<GameObject> inRangeEnemies = new List<GameObject>();

    public enum Relics
    {
        Fire,
        Ice,
        Wind,
        none
    }
    public Relics currentRelic = Relics.none;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (input.currentControlScheme == "Gamepad")
        {
            if (usingIndicator && !shoting)
            {
                indicatorObj.SetActive(true);
                Vector3 indicatorRotation = new Vector3(mousePos.x, 0, mousePos.y).normalized;
                indicatorObj.transform.rotation = Quaternion.LookRotation(indicatorRotation);
            }
            else
                indicatorObj.SetActive(false);
        }
    }

    public void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.gamePaused && !shoting && !attacking && !GameManager.Instance.inMenu && GameManager.Instance.gamePausable)
        {
            GameManager.Instance.playerCannotMove = true;
            GetComponent<PlayerAnimation>().Attack();
            //StartCoroutine(FinishAttack());
            attacking = true;
        }
    }

    IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(0.5f);

        GetComponent<PlayerAnimation>().Idle();
    }

    public void DistanceAttack(InputAction.CallbackContext context)
    {
        if (context.started && shotable && !GameManager.Instance.gamePaused && !GameManager.Instance.inMenu && GameManager.Instance.gamePausable)
            StartCoroutine(Shooting());
    }

    public void RelicAttack(InputAction.CallbackContext context)
    {
        if (context.started && relicUsable && GameManager.Instance.currentRelicSlots > -1 && !GameManager.Instance.gamePaused && !GameManager.Instance.inMenu && GameManager.Instance.gamePausable)
        {
            GameManager.Instance.playerCannotMove = true;
            GetComponent<PlayerAnimation>().RelicAttack();
            relicUsable = false;
        }
    }

    public void SelectCurrentRelic()
    {
        switch (currentRelic)
        {
            case Relics.Fire:
                FireRelic();
                break;
            case Relics.Ice:
                IceRelic();
                break;
            case Relics.Wind:
                WindRelic();
                break;
        }
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();

        if (input.currentControlScheme == "Keyboard")
            indicatorObj.SetActive(false);
        else if (input.currentControlScheme == "Gamepad")
        {
            if (context.started)
                usingIndicator = true;
            if (context.canceled)
                usingIndicator = false;

            //if (usingIndicator && !shoting)
            //{
            //    indicatorObj.SetActive(true);
            //    Vector3 indicatorRotation = new Vector3(mousePos.x, 0, mousePos.y).normalized;
            //    //indicatorObj.transform.rotation = Quaternion.LookRotation(indicatorRotation);
            //    indicatorObj.transform.rotation = Quaternion.Slerp(indicatorObj.transform.rotation, Quaternion.LookRotation(indicatorRotation), 10f * Time.deltaTime );
            //}
            //else
            //    indicatorObj.SetActive(false);
        }
    }

    public void ChangeRelic(InputAction.CallbackContext context)
    {
        if (UIManager.Instance.IsRotating) return;

        if (context.started && GameManager.Instance.currentRelicSlots >= 0 && GameManager.Instance.gamePausable)
        {
            float input = context.ReadValue<float>();

            if (input < 0)
                relicSlot++;
            else if (input > 0)
                relicSlot--;

            if (relicSlot > GameManager.Instance.currentRelicSlots)
                relicSlot = 0;
            else if (relicSlot < 0)
                relicSlot = GameManager.Instance.currentRelicSlots;

            if (GameManager.Instance.currentRelicSlots > 0)
                UIManager.Instance.gameObject.GetComponent<AudioSource>().PlayOneShot(relicRotationClip, 1f);

            switch (relicSlot)
            {
                case 0:
                    currentRelic = Relics.Fire;
                    UIManager.Instance.ChangeRelicInfo("Fire");
                    UIManager.Instance.UpdateRelicRotation(relicSlot);
                    break;
                case 1:
                    currentRelic = Relics.Ice;
                    UIManager.Instance.ChangeRelicInfo("Ice");
                    UIManager.Instance.UpdateRelicRotation(relicSlot);
                    break;
                case 2:
                    currentRelic = Relics.Wind;
                    UIManager.Instance.ChangeRelicInfo("Wind");
                    UIManager.Instance.UpdateRelicRotation(relicSlot);
                    break;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            float randomNum = Random.Range(0, 100);

            if (randomNum <= StatsManager.Instance.criticalChance)
                other.gameObject.GetComponent<IDamageable>().TakeDamage((-StatsManager.Instance.damage * StatsManager.Instance.damageMultiplyer), false);
            else
                other.gameObject.GetComponent<IDamageable>().TakeDamage(-StatsManager.Instance.damage, false);
        }
    }

    IEnumerator Shooting()
    {
        shotable = false;
        shoting = true;
        indicatorObj.SetActive(false);
        GameManager.Instance.playerCannotMove = true;

        shots++;

        Vector3 shotDirection = Vector3.zero;
        Quaternion targetRotation = Quaternion.identity;

        if (input.currentControlScheme == "Keyboard")
        {
            Ray screenRay = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(screenRay, out RaycastHit hit, Mathf.Infinity, rayMask))
                collidePosition = hit.point;

            Vector3 bulletPosition = shotPoint.position;
            Vector3 shotPosition = new Vector3(collidePosition.x, bulletPosition.y, collidePosition.z);
            shotDirection = shotPosition - bulletPosition;
            Vector3 collideInPlayerFoot = new Vector3(collidePosition.x, transform.position.y, collidePosition.z);

            Vector3 playerShotDirection = (collideInPlayerFoot - transform.position).normalized;

            targetRotation = Quaternion.LookRotation(playerShotDirection);
        }
        else if (input.currentControlScheme == "Gamepad")
        {
            Vector3 inputDirection = new Vector3(mousePos.x, 0, mousePos.y);
            shotDirection = inputDirection.normalized;
            targetRotation = Quaternion.LookRotation(shotDirection);

            if (targetRotation == Quaternion.identity)
            {
                Vector3 playerDirection = transform.forward;
                shotDirection = playerDirection.normalized;
                targetRotation = Quaternion.LookRotation(shotDirection);
            }
        }

        GetComponent<PlayerMovement>().ChangeRotation(targetRotation);
        GetComponent<PlayerAnimation>().Shoot();

        while (rb.rotation != targetRotation)
        {
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 15 * Time.deltaTime);

            if (Quaternion.Angle(rb.rotation, targetRotation) <= 5)
                rb.rotation = targetRotation;

            yield return null;
        }

        yield return new WaitForSeconds(0.75f);

        GameObject bullet = Instantiate(bulletPref, shotPoint.position, targetRotation);

        UIManager.Instance.ChangeBulletCount(6 - shots);

        bullet.GetComponent<Bullet>().GetShotDirection(shotDirection);

        StartCoroutine(ShootCooldown());
        Recoil();

        yield return new WaitForSeconds(0.25f);

        GameManager.Instance.playerCannotMove = false;
        shoting = false;
    }

    void Recoil()
    {
        rb.AddForce(transform.forward * -1 * recoilForce, ForceMode.Impulse);
    }

    public void FireRelic()
    {
        GetComponent<AudioSource>().PlayOneShot(relicClips[0], 2f);

        StartCoroutine(SpawnFireWisps());

        GameManager.Instance.playerCannotMove = false;

        inRangeEnemies.Clear();

        foreach (Collider burnableCollider in Physics.OverlapSphere(transform.position, fireDistance, burnableMask))
        {
            inRangeEnemies.Add(burnableCollider.gameObject);
        }

        StartCoroutine(RelicCoolDown());
        StartCoroutine(BurningEnemy());
    }

    //IEnumerator FireRelic()
    //{
    //    relicUsable = false;

    //    GameManager.Instance.playerCannotMove = true;

    //    yield return new WaitForSeconds(1.5f);

    //    StartCoroutine(SpawnFireWisps());

    //    GameManager.Instance.playerCannotMove = false;

    //    inRangeEnemies.Clear();

    //    foreach (Collider burnableCollider in Physics.OverlapSphere(transform.position, fireDistance, burnableMask))
    //    {
    //        inRangeEnemies.Add(burnableCollider.gameObject);
    //    }

    //    StartCoroutine(RelicCoolDown());
    //    StartCoroutine(BurningEnemy());
    //}

    IEnumerator SpawnFireWisps()
    {
        Vector3 spawnCenter = transform.position + Vector3.up * 0.5f; 

        for (int i = 0; i < totalWisps; i++)
        {
            float angle = (360f / totalWisps) * i * Mathf.Deg2Rad;
            StartCoroutine(MoveWispInSpiral(spawnCenter, angle));
            // yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }

    IEnumerator MoveWispInSpiral(Vector3 center, float angleOffset)
    {
        GameObject wisp = Instantiate(fireWispPrefab, center, Quaternion.identity);
        Transform wispTransform = wisp.transform;

        float t = 0f;

        Quaternion initialRotation = transform.rotation;

        while (t < spiralDuration)
        {
            float normalizedTime = t / spiralDuration;

            float angle = angleOffset + (normalizedTime * spiralRotations * 2 * Mathf.PI);
            float radius = normalizedTime * spiralOutwardDistance;

            Vector3 localOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Vector3 rotatedOffset = initialRotation * localOffset;
            
            wispTransform.position = center + rotatedOffset;

            t += Time.deltaTime;
            yield return null;
        }

        ParticleSystem ps = wisp.GetComponent<ParticleSystem>();
        if (ps != null) ps.Stop();

        Light coreLight = wisp.transform.Find("FireRelicCore")?.GetComponent<Light>();

        if (coreLight != null)
        {
            float duration = 1f;
            float elapsed = 0f;
            float startIntensity = coreLight.intensity;

            while (elapsed < duration)
            {
                coreLight.intensity = Mathf.Lerp(startIntensity, 0f, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            coreLight.intensity = 0f;
        }

        Destroy(wisp, 3f);
    }


    void IceRelic()
    {
        GetComponent<AudioSource>().PlayOneShot(relicClips[1], 2f);

        GameObject iceBullet = Instantiate(iceObj, shotPoint.transform.position, Quaternion.identity);
        iceBullet.GetComponent<IceRelic>().GetDirection(transform.forward);

        GameManager.Instance.playerCannotMove = false;
        StartCoroutine(RelicCoolDown());
    }

    void WindRelic()
    {
        GetComponent<AudioSource>().PlayOneShot(relicClips[2], 2f);

        GameObject windBullet = Instantiate(windObj, shotPoint.transform.position, Quaternion.identity);
        windBullet.GetComponent<WindRelic>().GetDirection(transform.forward);

        GameManager.Instance.playerCannotMove = false;
        StartCoroutine(RelicCoolDown());
    }

    IEnumerator BurningEnemy()
    {
        for (int i = 0; i < 5; i++)
        {
            foreach (GameObject burnableObj in inRangeEnemies)
            {
                burnableObj.GetComponent<IDamageable>().TakeDamage(-0.25f, true);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator ShootCooldown()
    {
        if (shots < 6)
        {
            UIManager.Instance.ActiveShootCooldown(1f / StatsManager.Instance.shootCadence);
            yield return new WaitForSeconds(1f / StatsManager.Instance.shootCadence);
        }
        if (shots >= 6)
        {
            yield return new WaitForSeconds(rechargeTime);
            shots = 0;
            UIManager.Instance.ChangeBulletCount(6 - shots);
        }

        shotable = true;
    }

    IEnumerator RelicCoolDown()
    {
        inRelicCooldown = true;

        UIManager.Instance.ActiveRelicCooldown(5f);
        yield return new WaitForSeconds(5f);

        inRelicCooldown = false;
        relicUsable = true;
    }
}
