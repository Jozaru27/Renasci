using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Dash))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool groundCheck;//

    [HideInInspector] public float inputFactor;
    [HideInInspector] public Vector3 movement;
    [HideInInspector] public Vector2 inputMovement;

    [Header("Player Movement and Camera")]
    [SerializeField] float force;
    [SerializeField] float rotationSpeed;
    [Header("Physics Control")]
    [SerializeField] float stayDrag;
    [SerializeField] float moveDrag;
    [SerializeField] GameObject groundChecker;

    float viewPos;
    bool inIdle;
    Vector3 playerMovement;
    Vector3 inputAngle;
    Quaternion rotationTarget = Quaternion.identity;
    Rigidbody rb;
    Dash dash;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dash = GetComponent<Dash>();
        inputFactor = 1;
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
        if (!GameManager.Instance.playerCannotMove)
        {
            if (inputMovement.magnitude >= 0.25f)
            {
                GetComponent<PlayerAnimation>().Run();
                playerMovement = new Vector3(inputMovement.x * inputFactor , 0, inputMovement.y * inputFactor);

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
        }
            
        if (rb.velocity.magnitude <= 0.1f)
            rb.velocity = Vector3.zero;
    }

    void RotateCharacter()
    {
        if (inputMovement.magnitude >= 0.25f && !GameManager.Instance.playerCannotMove)
        {
            //viewPos = Mathf.Atan2(inputMovement.x, inputMovement.y) * Mathf.Rad2Deg;
            //viewPos = Mathf.Round(viewPos * 100) / 100;

            inputAngle = new Vector3(inputMovement.x * inputFactor, 0, inputMovement.y * inputFactor);

            if (inputAngle != Vector3.zero)
                rotationTarget = Quaternion.LookRotation(inputAngle);
        }

        //Quaternion target = Quaternion.Euler(0, viewPos, 0);

        if (Quaternion.Angle(rb.rotation, rotationTarget) > 0.15f)
        {
            //rb.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
            rb.rotation = Quaternion.Slerp(rb.rotation, rotationTarget, rotationSpeed * Time.deltaTime);

            if (Math.Abs(rb.rotation.y - rotationTarget.y) <= 0.01f)
                rb.rotation = rotationTarget;
        }
    }

    void PhysicsControl()
    {
        if (groundCheck)
        {
            if (groundChecker.GetComponent<GroundCheck>().grounded)
            {
                if (inputMovement == new Vector2(0, 0))
                    rb.drag = stayDrag;
                else
                    rb.drag = moveDrag;
            }
            else
                rb.drag = 0;
        }
        else
        {
            if (inputMovement == new Vector2(0, 0))
                rb.drag = stayDrag;
            else
                rb.drag = moveDrag;
        }
    }

    public void ChangeRotation(Quaternion newTarget)
    {
        rotationTarget = newTarget;
    }
}
