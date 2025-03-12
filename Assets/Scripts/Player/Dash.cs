using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [HideInInspector] public bool dashing;

    [SerializeField] float dashForce;
    [SerializeField] float dashTime;
    [SerializeField] float maxDashSpeed;

    Rigidbody rb;
    PlayerMovement pMov;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pMov = GetComponent<PlayerMovement>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !dashing)
        {
            rb.AddForce(pMov.movement * dashForce, ForceMode.Impulse);

            Vector3 velocity = rb.velocity;
            if (rb.velocity.magnitude > maxDashSpeed)
                rb.velocity = velocity.normalized * maxDashSpeed;

            StartCoroutine(Dashing());
        }
    }

    IEnumerator Dashing()
    {
        dashing = true;

        yield return new WaitForSeconds(dashTime);

        dashing = false;
    }
}
