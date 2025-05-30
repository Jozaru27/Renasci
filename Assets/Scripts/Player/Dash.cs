using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [HideInInspector] public bool dashing;

    [SerializeField] float dashForce;
    [SerializeField] float dashTime;
    [SerializeField] float maxDashSpeed;
    [SerializeField] GameObject dashDust;

    bool canDash = true;
    Rigidbody rb;
    PlayerMovement pMov;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pMov = GetComponent<PlayerMovement>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !dashing && canDash && GameManager.Instance.gamePausable)
        {
            if (pMov.inputMovement.magnitude > 0)
            {
                StartCoroutine(DashCooldown());
                StartCoroutine(Dashing());
                GetComponent<PlayerHealth>().ChangeVencibleColor();
                StartCoroutine(GetComponent<PlayerHealth>().InvencibleDash(dashTime));

                UIManager.Instance.ResetStamina();

                rb.AddForce(pMov.movement * dashForce, ForceMode.Impulse);
            }

            Vector3 velocity = rb.velocity;
            if (rb.velocity.magnitude > maxDashSpeed)
                rb.velocity = velocity.normalized * maxDashSpeed;
        }
    }

    IEnumerator Dashing()
    {
        GetComponent<PlayerAnimation>().Dash();
        dashDust.GetComponent<ParticleSystem>().Play();
        dashing = true;

        yield return new WaitForSeconds(dashTime);

        GetComponent<PlayerAnimation>().Idle();
        dashing = false;
    }

    IEnumerator DashCooldown()
    {
        canDash = false;

        yield return new WaitForSeconds(StatsManager.Instance.dashCooldown);

        canDash = true;
    }
}
