using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;

    bool canAttack = true;
    Vector2 mousePos;

    public void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.started && canAttack)
            GetComponent<PlayerAnimation>().Attack();
    }

    public void DistanceAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameObject bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3 (mousePos.x, mousePos.y, 0));
            Vector3 shotPosition = new Vector3(mouseWorld.x, transform.position.y, mouseWorld.y);

            Vector3 shootDirection = shotPosition - transform.position;

            //
            Vector3 eo = new Vector3(mousePos.x, 0, mousePos.y);
            //shootDirection = eo - transform.position;
            //

            bullet.GetComponent<Bullet>().GetMousePosition(shootDirection);
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
        //if (other.gameObject.CompareTag("Enemy"))
            //ChangeHealth
    }
}
