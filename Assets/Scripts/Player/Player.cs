using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;
    private float inputX;
    private float inputY;
    private Vector2 movementInput;

    private Animator[] animators;
    private bool isMoving;

    private Transform tf;
    [SerializeField]private bool isInputDisable;

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadedEvent += OnBeforeSceneUnloadedEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.MoveToPositionEvent += OnMoveToPositionEvent;
        EventHandler.MouseClickEvent += OnMouseClickEvent;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadedEvent -= OnBeforeSceneUnloadedEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.MoveToPositionEvent -= OnMoveToPositionEvent;
        EventHandler.MouseClickEvent -= OnMouseClickEvent;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
        tf = GetComponent<Transform>();
    }

    private void Update()
    {
        if (isInputDisable == false)
            PlayerInput();
        else
            isMoving = false;
        SwitchAnimation();
    }

    private void FixedUpdate()
    {
        if (isInputDisable == false)
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
        rb.MovePosition(rb.position + speed * Time.deltaTime * movementInput);

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

    private void OnMoveToPositionEvent(Vector3 targetPos)
    {
        transform.position = targetPos;
    }

    private void OnBeforeSceneUnloadedEvent()
    {
        isInputDisable = true;
    }
    private void OnAfterSceneLoadedEvent()
    {
        isInputDisable= false;
    }

    private void OnMouseClickEvent(Vector3 pos, ItemDetails itemDetails)
    {
        //TODO: Animation


        EventHandler.CallExecuteActionAfterAnimation(pos, itemDetails);
    }


}
