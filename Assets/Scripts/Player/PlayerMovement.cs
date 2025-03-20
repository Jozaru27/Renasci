using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Dash))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Vector3 movement;
    [HideInInspector] public Vector2 inputMovement;

    [Header("Player Movement and Camera")]
    [SerializeField] float force;
    [SerializeField] float maxSpeed;
    [SerializeField] float maxSlopeAngle;
    [SerializeField] float rotationSpeed;
    [Header("Physics Control")]
    [SerializeField] float stayDamping;
    [SerializeField] float moveDamping;
    [SerializeField] float slopeForce;

    float viewPos;
    bool inIdle;
    Vector3 playerMovement;
    Rigidbody rb;
    Dash dash;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dash = GetComponent<Dash>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.gameOver && !GameManager.Instance.gameWin)
            inputMovement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        RotateCharacter();
        PhysicsControl();
    }

    void MoveCharacter()
    {
        if (inputMovement.magnitude >= 0.25f)
        {
            GetComponent<PlayerAnimation>().Run();
            playerMovement = new Vector3(inputMovement.x, 0, inputMovement.y);

            movement = playerMovement;
            Vector3 velocity = rb.velocity;

            if (rb.velocity.magnitude < StatsManager.Instance.movementSpeed)
                rb.AddForce(movement * force, ForceMode.Acceleration);
            else if (!dash.dashing)
                rb.velocity = velocity.normalized * StatsManager.Instance.movementSpeed;

            inIdle = false;
        }
        else if (!inIdle)
        {
            GetComponent<PlayerAnimation>().Idle();
            inIdle = true;
        }
            
        if (rb.velocity.magnitude <= 0.1f)
            rb.velocity = Vector3.zero;
    }

    void RotateCharacter()
    {
        if (inputMovement.magnitude >= 0.25f)
        {
            viewPos = Mathf.Atan2(inputMovement.x, inputMovement.y) * Mathf.Rad2Deg;
            viewPos = Mathf.Round(viewPos * 100) / 100;
        }

        Quaternion target = Quaternion.Euler(0, viewPos, 0);

        if (Quaternion.Angle(rb.rotation, target) > 0.15f)
            rb.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
    }

    void PhysicsControl()
    {
        if (inputMovement == new Vector2(0, 0))
        {
            rb.drag = stayDamping;
        }
        else
            rb.drag = moveDamping;
    }
}
