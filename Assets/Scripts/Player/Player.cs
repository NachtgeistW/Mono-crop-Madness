using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    public float speed;
    private float inputX;
    private float inputY;
    private Vector2 movementInput;

    private Animator[] animators;
    private bool isMoving;

    private Transform transform;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        PlayerInput();
        SwitchAnimation();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void PlayerInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        // limit player's moving speed 
        if (inputX != 0 && inputY != 0)
        {
            inputX *= 0.6f;
            inputY *= 0.6f;
        }

        //Press shift to walk
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputX *= 0.5f;
            inputY *= 0.5f;
        }
        movementInput = new Vector2(inputX, inputY);

        isMoving = movementInput != Vector2.zero;
    }

    private void Movement()
    {
        rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime * movementInput);

    }

    private void SwitchAnimation()
    {
        foreach(Animator animator in animators)
        {
            animator.SetBool("IsMoving", isMoving);
            if(isMoving)
            {
                animator.SetFloat("InputX", inputX);
                animator.SetFloat("InputY", inputY);
            }
        }
    }
}
