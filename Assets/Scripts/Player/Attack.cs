using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;

    bool canAttack = true;
    Vector2 mousePos;
    Collider trigger;

    private void Start()
    {
        trigger = GetComponent<Collider>();
    }

    public void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.started && canAttack)
            StartCoroutine(EnablingTrigger());
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

    IEnumerator EnablingTrigger()
    {
        canAttack = false;
        trigger.enabled = true;

        yield return new WaitForSeconds(0.5f);

        trigger.enabled = false;
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Enemy"))
            //ChangeHealth
    }
}
