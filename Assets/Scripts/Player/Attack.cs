using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;

    Vector2 mousePos;

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
        //if (context.started)
        //{
        //    GameObject bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);

        //    Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3 (mousePos.x, mousePos.y, 0));
        //    Vector3 shotPosition = new Vector3(mouseWorld.x, transform.position.y, mouseWorld.y);

        //    Vector3 shootDirection = shotPosition - transform.position;

        //    //
        //    Vector3 eo = new Vector3(mousePos.x, 0, mousePos.y);
        //    //shootDirection = eo - transform.position;
        //    //

        //    bullet.GetComponent<Bullet>().GetMousePosition(shootDirection);
        //}
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
        {
            //
            if (other.gameObject.name == "Enemigo [Placeholder]")
            {
                Vector3 impulseDirection = other.gameObject.transform.position - transform.position;
                impulseDirection = new Vector3(impulseDirection.x, 0, impulseDirection.y);
                other.gameObject.GetComponent<Rigidbody>().AddForce(impulseDirection.normalized * 10, ForceMode.Impulse);
                other.gameObject.GetComponent<EnemyTest>().TakeDamage(-1);
            }//
            else
                other.gameObject.GetComponent<IDamageable>().TakeDamage(-1);
        }
    }
}
