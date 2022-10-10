using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public CapsuleCollider CapsuleCollider;
    public Rigidbody Rigidbody;

    bool isGrounded;
    public float walkSpeed;
    public float sprintSpeed;

    public float crouchSpeed;
    public float crouchScale;
    private float startYScale;

    public float jumpForce = 10f;
    public float gravity = -9.8f;

    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    Vector3 moveDirection;
    Vector2 currectInput;

    private RaycastHit slopeHit;
    public float slopeForce;
    public float slopeForceRayLength;
    public float slopeSpeed = 2f;
    public Vector3 hitPointNormal;
    bool isSliding;

    public bool isSwimming;
    public float swimSpeed;
    public Transform target;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    // Start is called before the first frame update
    void Start()
    {
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f);

        if (isSwimming)
        {
            Swimming();
        }
        else
        {
            HandleMovementInput();
            ApplyFinalMovements();
            StateHandles();
            CrouchScale();
            SlideOnSlope();

            if (currectInput.x != 0 || currectInput.y != 0 && OnSlope())
            {
                controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
            }
        }
    }

    void HandleMovementInput()
    {
        currectInput = new Vector2(walkSpeed * Input.GetAxis("Vertical"), walkSpeed * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currectInput.x) + (transform.TransformDirection(Vector3.right) * currectInput.y);
        moveDirection.y = moveDirectionY;
        if (!isSliding)
        {
            Jump();
        }
    }

    void ApplyFinalMovements()
    {
        if (!controller.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        if (controller.isGrounded && isSliding)
        {
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
    }

    void CrouchScale()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    void StateHandles()
    {
        if (controller.isGrounded && Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            walkSpeed = crouchSpeed;
        }
        else if (controller.isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            walkSpeed = sprintSpeed;
        }
        else if (controller.isGrounded)
        {
            state = MovementState.walking;
            walkSpeed = 7;
        }
        else
        {
            state = MovementState.air;
        }
    }

    bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, slopeForceRayLength))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

    void SlideOnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, slopeForceRayLength))
        {
            hitPointNormal = slopeHit.normal;
            isSliding = Vector3.Angle(hitPointNormal, Vector3.up) > controller.slopeLimit;
        }
        else
        {
            isSliding = false;
        }
    }

    public void Swimming()
    {
        if (isSwimming != true)
        {
            controller = GetComponent<CharacterController>();
            CapsuleCollider = GetComponent<CapsuleCollider>();
            controller.enabled = true;
            CapsuleCollider.enabled = false;

            if (Rigidbody.useGravity != true)
            {
                Rigidbody.useGravity = true;
            }
        }
        else
        {
            controller.enabled = false;
            CapsuleCollider.enabled = true;

            if (Rigidbody.useGravity == true)
            {
                Rigidbody.useGravity = false;
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                transform.position += target.forward * swimSpeed * Time.deltaTime;
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                transform.position -= target.forward * swimSpeed * Time.deltaTime;
            }
        }
    }

    public void ResetVelocity()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
    }
}
