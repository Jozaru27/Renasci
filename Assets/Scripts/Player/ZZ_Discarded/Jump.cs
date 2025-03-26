using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    public bool grounded = true;
    [SerializeField] float jumpForce;

    bool onSlope;
    bool onGround;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Vector3 velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

        if (context.started && grounded)
            rb.AddForce(velocity, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            onSlope = false;
            onGround = true;
        }
        if (other.gameObject.CompareTag("Stairs"))
        {
            grounded = true;
            onSlope = true;
            onGround = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") && !onSlope)
        {
            grounded = false;
            onGround = false;
        }
        if (other.gameObject.CompareTag("Stairs") && !onGround)
        {
            grounded = false;
            onSlope = false;
        }
    }
}
