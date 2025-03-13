using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Dash))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Vector3 movement;

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
    bool firstStepOnSlope;
    Vector2 inputMovement;
    Vector3 playerMovement;
    RaycastHit slopeHit;
    Rigidbody rb;
    Dash dash;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dash = GetComponent<Dash>();
        maxSpeed = StatsManager.Instance.movementSpeed;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        PhysicsControl();
    }

    private void Update()
    {
        RotateCharacter();
    }

    void MoveCharacter()
    {
        playerMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        movement = playerMovement;
        Vector3 velocity = rb.velocity;

        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(movement * force, ForceMode.Acceleration);
        else if (!dash.dashing)
            rb.velocity = velocity.normalized * maxSpeed;

        if (rb.velocity.magnitude <= 0.1f)
            rb.velocity = new Vector3(0, 0, 0);
    }

    void RotateCharacter()
    {
        if (inputMovement.magnitude >= 0.75f)
            viewPos = Mathf.Atan2(inputMovement.x, inputMovement.y) * Mathf.Rad2Deg;

        Quaternion target = Quaternion.Euler(0, viewPos, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
    }

    void PhysicsControl()
    {
        //if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, (GetComponent<CapsuleCollider>().height / 2) + 0.3f))
        //    pivot.transform.position = slopeHit.point;

        if (inputMovement == new Vector2(0, 0))
        {
            rb.drag = stayDamping;
        }
        else
            rb.drag = moveDamping;
    }
}
