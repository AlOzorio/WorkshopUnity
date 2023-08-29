using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public AudioSource jumpy;

    private Vector2 direction;
    private Rigidbody2D rb;
    private Animator animator;

    private bool canJump = true;
    private float accel = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move(direction);

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            if (jumpy != null)
            {
                jumpy.Play();
            }

            if (animator != null)
            {
                animator.Play("Jump");
            }

            canJump = false;
            animator.SetBool("canJump", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            canJump = true;
            animator.SetBool("canJump", true);
        }
    }

    public void Move(Vector2 direction)
    {
        Vector2 targetVelocity;

        if (direction.x != 0)
        {
            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            targetVelocity = new Vector2(speed * direction.x, rb.velocity.y);
            rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.deltaTime * accel);
        }
        else
        {
            targetVelocity = new Vector2(0, rb.velocity.y);
            rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.deltaTime * accel);
        }
    }
}
