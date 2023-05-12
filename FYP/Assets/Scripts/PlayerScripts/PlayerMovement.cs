using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public CapsuleCollider CapsuleCollider;
    public Rigidbody Rigidbody;

    bool isWalking;
    bool isGrounded;
    bool isRan;
    bool isJumped;
    bool isCrounched;
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

    public AudioSource WalkingInGrass;
    public AudioSource RunningInGrass;

    public Vector3 currentVector = Vector3.up;
    public float CurrentForce = 0;
    public float MaxForce = 5;
    public bool isflying;
    public float clickTime;

    public Animator characterAnim;

    public Image FillBar;
    //public ShopManager shopManager;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air,
        standing
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
            //CrouchScale();
            SlideOnSlope();
            FootStep();

            if (currectInput.x != 0 || currectInput.y != 0 && OnSlope())
            {
                controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
            }
        }

        if (ShopManager.instance.haveJetpack)
        {
            FillBar.fillAmount = MaxForce;

            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isJumped = true;
                    clickTime = Time.time;
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    MaxForce -= Time.deltaTime * 10;
                }

                if (Input.GetKey(KeyCode.Space) && MaxForce > 0 && (Time.time - clickTime) > 0.5)
                {
                    MaxForce -= Time.deltaTime / 3;

                    if (CurrentForce < 1)
                    {
                        CurrentForce += Time.deltaTime * 10;
                    }
                    else
                    {
                        CurrentForce = 1;
                    }
                }
            }

            if (MaxForce < 0 && CurrentForce > 0)
            {
                CurrentForce -= Time.deltaTime;
            }

            if (!Input.GetKey(KeyCode.Space))
            {
                if (CurrentForce > 0)
                {
                    CurrentForce -= Time.deltaTime;
                    
                }
                else
                {
                    CurrentForce = 0;
                }
                if (MaxForce < 1)
                {

                    MaxForce += Time.deltaTime / 3;
                }
                else
                {
                    MaxForce = 1;
                }
            }

            if (CurrentForce > 0)
            {
                UseJetPack();
                isflying = true;
                Rigidbody.useGravity = false;
            }
            else
            {

                isflying = false;
                Rigidbody.useGravity = true;
            }
        }
    }

    void HandleMovementInput()
    {
        //walking
        currectInput = new Vector2(walkSpeed * Input.GetAxis("Vertical"), walkSpeed * Input.GetAxis("Horizontal"));
        if (isRan)
        {
            characterAnim.SetBool("isRan", true);
            characterAnim.SetBool("isWalked", false);
        }
            
        else if (isCrounched)
        {
            characterAnim.SetBool("isCrounched", true);
            //characterAnim.SetBool("isWalked", false);
            if (currectInput.x !=0 || currectInput.y != 0)
            {
                characterAnim.SetBool("isWalked", true);
            }
            else
                characterAnim.SetBool("isWalked", false);
        }
            
        else
            characterAnim.SetBool("isWalked", true);


        isWalking = true;
        if (currectInput.x == 0 && currectInput.y == 0)
        {
            isWalking = false;
            if(isRan)
                characterAnim.SetBool("isRan", false);
            else if (isCrounched)
                characterAnim.SetBool("isCrounched", false);
            else
               characterAnim.SetBool("isWalked", false);
        }

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currectInput.x) + (transform.TransformDirection(Vector3.right) * currectInput.y);
        moveDirection.y = moveDirectionY;
        if (!isSliding && !isflying)
        {
            isJumped = true;
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
        if (!isflying)
        {
            controller.Move(moveDirection * Time.deltaTime);

            gravity = -9.8f;
        }
        else
        {
            controller.Move(Vector3.zero * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            if(isJumped)
            {
                characterAnim.SetTrigger("isJumped");
            }

            moveDirection.y = jumpForce;
            isJumped = false;
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
            isCrounched = true;
            state = MovementState.crouching;
            walkSpeed = crouchSpeed;
            characterAnim.SetBool("isCrounched", true);
        }
        else if (controller.isGrounded && Input.GetKey(sprintKey))
        {
            isRan = true;
            state = MovementState.sprinting;
            walkSpeed = sprintSpeed;
            characterAnim.SetBool("isRan", true);
        }
        else if (controller.isGrounded && isWalking)
        {
            state = MovementState.walking;
            walkSpeed = 7;
        }
        else if (controller.isGrounded)
        {
            isCrounched = false;
            isRan = false;
            state = MovementState.standing;
            characterAnim.SetBool("isRan", false);
            characterAnim.SetBool("isCrounched", false);

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

    public void FootStep()
    {
        if (state == MovementState.walking)
        {
            if (!WalkingInGrass.isPlaying && !RunningInGrass.isPlaying)
            {
                WalkingInGrass.Play();
            }
        }
        else if (state == MovementState.sprinting)
        {
            if (!WalkingInGrass.isPlaying && !RunningInGrass.isPlaying)
            {
                RunningInGrass.Play();
            }
        }
    }

    public void UseJetPack()
    {
        currentVector = Vector3.up;

        currentVector += transform.right * Input.GetAxis("Horizontal");
        currentVector += transform.forward * Input.GetAxis("Vertical");

        if (Input.GetKey(sprintKey))
        {
            controller.Move((currentVector * 7 * Time.deltaTime - controller.velocity * Time.deltaTime) * CurrentForce);
        }
        else
        {
            controller.Move((currentVector * walkSpeed * Time.deltaTime - controller.velocity * Time.deltaTime) * CurrentForce);
        }

        gravity = -4.9f;
    }
}
