using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] float rechargeTime;
    [SerializeField] float recoilForce;
    [SerializeField] GameObject bulletPref;
    [SerializeField] Transform shotPoint;
    [SerializeField] LayerMask rayMask;

    int shots;
    bool shotable = true;
    bool shoting;
    Vector2 mousePos;
    Vector3 collidePosition;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.gamePaused && !shoting)
        {
            GameManager.Instance.playerCannotMove = true;
            //GetComponent<PlayerAnimation>().Attack();
        }
    }

    public void DistanceAttack(InputAction.CallbackContext context)
    {
        if (context.started && shotable && !GameManager.Instance.gamePaused)
            StartCoroutine(Shooting());
    }

    public void RelicAttack(InputAction.CallbackContext context)
    {

    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    public void ChangeRelic(InputAction.CallbackContext context)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            other.gameObject.GetComponent<IDamageable>().TakeDamage(-StatsManager.Instance.damage);
    }

    IEnumerator Shooting() // Placeholder
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

        Vector3 playerShotDirection = collidePosition - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(playerShotDirection);

        GetComponent<PlayerMovement>().ChangeRotation(targetRotation);
        //GetComponent<PlayerAnimation>().Shoot();

        while (rb.rotation != targetRotation)
        {
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 15 * Time.deltaTime);

            if (Quaternion.Angle(rb.rotation, targetRotation) <= 5)
                rb.rotation = targetRotation;

            yield return null;
        }

        yield return new WaitForSeconds(0.25f);

        GameObject bullet = Instantiate(bulletPref, shotPoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().GetShotDirection(shotDirection);

        StartCoroutine(ShootCooldown());
        Recoil();

        yield return new WaitForSeconds(0.25f);

        GameManager.Instance.playerCannotMove = false;
        shoting = false;
    }

    IEnumerator ShootCooldown()
    {
        if (shots < 6)
            yield return new WaitForSeconds(StatsManager.Instance.shootCadence);
        if (shots >= 6)
        {
            yield return new WaitForSeconds(rechargeTime);
            shots = 0;
        }

        shotable = true;
    }

    void Recoil()
    {
        rb.AddForce(transform.forward * -1 * recoilForce, ForceMode.Impulse);
    }
    
}
