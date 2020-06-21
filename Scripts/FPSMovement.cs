using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPSMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundMask;

    Animator anim;
    Vector3 velocity;
    public Vector3 move;
    bool isGrounded;
    public bool isRunning;

    void Awake()
    {
        anim = GameObject.Find("BodyAnimation").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Run();
        CheckForCround();
        Jump();
    }

    private void CheckForCround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Walk()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = (transform.right * x + transform.forward * z).normalized;
        controller.Move(move * speed * Time.deltaTime);
        anim.SetFloat("Velocity", move.sqrMagnitude);
        
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            isRunning = true;
            controller.Move(move * runSpeed * Time.deltaTime);
            anim.SetBool("Run", isRunning);
        }
        else
        {
            isRunning = false;
            anim.SetBool("Run", isRunning);
        }
    }



}
