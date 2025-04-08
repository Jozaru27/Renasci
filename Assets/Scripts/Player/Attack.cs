using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [Header("Distance Attack")]
    [SerializeField] float rechargeTime;
    [SerializeField] float recoilForce;
    [SerializeField] GameObject bulletPref;
    [SerializeField] Transform shotPoint;
    [SerializeField] LayerMask rayMask;

    [Header("Relic Behaviour")]
    [SerializeField] float fireDistance;
    [SerializeField] LayerMask burnableMask;

    int shots;
    int relicSlot = 0;
    bool shotable = true;
    bool shoting;
    bool relicUsable = true;
    Vector2 mousePos;
    Vector3 collidePosition;
    Rigidbody rb;

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
    }

    public void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.gamePaused && !shoting)
        {
            GameManager.Instance.playerCannotMove = true;
            GetComponent<PlayerAnimation>().Attack();
        }
    }

    public void DistanceAttack(InputAction.CallbackContext context)
    {
        if (context.started && shotable && !GameManager.Instance.gamePaused)
            StartCoroutine(Shooting());
    }

    public void RelicAttack(InputAction.CallbackContext context)
    {
        if (context.started && relicUsable)
        {
            switch (currentRelic)
            {
                case Relics.Fire:
                    StartCoroutine(FireRelic());
                    break;
                case Relics.Ice:
                    IceRelic();
                    break;
                case Relics.Wind:
                    WindRelic();
                    break;
            }
        }
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    public void ChangeRelic(InputAction.CallbackContext context)
    {
        if (context.started && GameManager.Instance.currentRelicSlots >= 0)
        {
            float input = context.ReadValue<float>();

            if (input < 0)
                relicSlot--;
            else if (input > 0)
                relicSlot++;

            if (relicSlot > GameManager.Instance.currentRelicSlots)
                relicSlot = 0;
            else if (relicSlot < 0)
                relicSlot = GameManager.Instance.currentRelicSlots;

            switch (relicSlot)
            {
                case 0:
                    currentRelic = Relics.Fire;
                    UIManager.Instance.ChangeRelicInfo("Fire");
                    break;
                case 1:
                    currentRelic = Relics.Ice;
                    UIManager.Instance.ChangeRelicInfo("Ice");
                    break;
                case 2:
                    currentRelic = Relics.Wind;
                    UIManager.Instance.ChangeRelicInfo("Wind");
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
        GameManager.Instance.playerCannotMove = true;

        shots++;

        Ray screenRay = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(screenRay, out RaycastHit hit, Mathf.Infinity, rayMask))
            collidePosition = hit.point;

        Vector3 bulletPosition = shotPoint.position;
        Vector3 shotPosition = new Vector3(collidePosition.x, bulletPosition.y, collidePosition.z);
        Vector3 shotDirection = shotPosition - bulletPosition;
        Vector3 collideInPlayerFoot = new Vector3(collidePosition.x, transform.position.y, collidePosition.z);

        Vector3 playerShotDirection = collideInPlayerFoot - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(playerShotDirection);

        GetComponent<PlayerMovement>().ChangeRotation(targetRotation);
        GetComponent<PlayerAnimation>().Shoot();

        while (rb.rotation != targetRotation)
        {
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 15 * Time.deltaTime);

            if (Quaternion.Angle(rb.rotation, targetRotation) <= 5)
                rb.rotation = targetRotation;

            yield return null;
        }

        yield return new WaitForSeconds(0.125f);

        GameObject bullet = Instantiate(bulletPref, shotPoint.position, Quaternion.identity);

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
    
    IEnumerator FireRelic()
    {
        GameManager.Instance.playerCannotMove = true;

        yield return new WaitForSeconds(1.5f);

        GameManager.Instance.playerCannotMove = false;

        foreach (Collider burnableCollider in Physics.OverlapSphere(transform.position, fireDistance, burnableMask))
        {
            inRangeEnemies.Add(burnableCollider.gameObject);
        }

        StartCoroutine(RelicCoolDown());
        StartCoroutine(BurningEnemy());
    }

    void IceRelic()
    {

    }

    void WindRelic()
    {

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
            yield return new WaitForSeconds(StatsManager.Instance.shootCadence);
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
        relicUsable = false;

        yield return new WaitForSeconds(5f);

        relicUsable = true;
    }
}
