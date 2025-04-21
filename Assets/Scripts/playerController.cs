using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class playerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runMultiplier = 1.5f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;

    private bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value); // Fixed casing
        }
    }

    private bool _isRunning = false;
    public bool isRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            animator.SetBool("IsRunning", value); // Fixed casing
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Debug check to ensure Animator is set
        if (animator == null)
        {
            Debug.LogError("Animator component missing on player GameObject!");
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = isRunning ? walkSpeed * runMultiplier : walkSpeed;
        rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);
    }

    // Input System: Movement callback
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    // Input System: Run callback
    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }
}
