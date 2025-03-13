using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [HideInInspector] public bool dashing;

    [SerializeField] float dashForce;
    [SerializeField] float dashTime;
    [SerializeField] float maxDashSpeed;

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
        if (context.performed && !dashing && canDash)
        {
            rb.AddForce(pMov.movement * dashForce, ForceMode.Impulse);

            Vector3 velocity = rb.velocity;
            if (rb.velocity.magnitude > maxDashSpeed)
                rb.velocity = velocity.normalized * maxDashSpeed;

            StartCoroutine(DashCooldown());
            StartCoroutine(Dashing());
        }
    }

    IEnumerator Dashing()
    {
        dashing = true;

        yield return new WaitForSeconds(dashTime);

        dashing = false;
    }

    IEnumerator DashCooldown()
    {
        canDash = false;

        yield return new WaitForSeconds(StatsManager.Instance.dashCooldown);

        canDash = true;
    }
}
