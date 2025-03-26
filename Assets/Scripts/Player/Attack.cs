using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] float rechargeTime;
    [SerializeField] GameObject bulletPref;
    [SerializeField] Transform shotPoint;
    [SerializeField] LayerMask rayMask;

    int shots;
    bool shotable = true;
    Vector2 mousePos;
    Vector3 collidePosition;

    public void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.gamePaused)
        {
            GameManager.Instance.playerCannotMove = true;
            GetComponent<PlayerAnimation>().Attack();
        }
    }

    public void DistanceAttack(InputAction.CallbackContext context)
    {
        if (context.started && shotable)
        {
            shots++;
            GameObject bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);

            Ray screenRay = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(screenRay,out RaycastHit hit, Mathf.Infinity, rayMask))
                collidePosition = hit.point;

            Vector3 bulletPosition = shotPoint.position;
            Vector3 shotPosition = new Vector3(collidePosition.x, bulletPosition.y, collidePosition.z);
            Vector3 shotDirection = shotPosition - bulletPosition;

            bullet.GetComponent<Bullet>().GetMousePosition(shotDirection);

            StartCoroutine(ShootCooldown());
        }
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

    IEnumerator ShootCooldown()
    {
        shotable = false;

        if (shots < 6)
            yield return new WaitForSeconds(StatsManager.Instance.shootCadence);
        if (shots >= 6)
        {
            yield return new WaitForSeconds(rechargeTime);
            shots = 0;
        }

        shotable = true;
    }
}
