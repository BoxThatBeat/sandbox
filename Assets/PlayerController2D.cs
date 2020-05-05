using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    GameObject attackHitBox1;

    [SerializeField]
    GameObject attackHitBox2;

    [SerializeField]
    GameObject attackHitBox4;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform groundCheckL;

    [SerializeField]
    private Transform groundCheckR;

    // Values
    [SerializeField]
    private float runSpeed = 1.5f;

    [SerializeField]
    private float jumpSpeed = 4f;

    private float dashSpeed = 3f;

    private bool isAttacking = false;

    private bool isGrounded;

    private bool isCrouched = false;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;

            // Select the attack animation 
            if (isGrounded)
            {

                if (isCrouched)
                {
                    // Crouch attack
                    animator.Play("Player_attack3");
                }
                else
                {
                    // Melee attack
                    int i = UnityEngine.Random.Range(1, 3);
                    animator.Play("Player_attack" + i);
                }

                


            }
            else
            {
                // Aerial attack 
                animator.Play("Player_attack4");
            }

            Invoke("ResetAttack", .5f);
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    private void FixedUpdate()
    {
        // Alters member value depending if the player is grounded or in the air 
        if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))    ||
            (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")))   ||
            (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;

            if (!isAttacking)
                animator.Play("Player_jump");
        }

        if (Input.GetKey("d") || Input.GetKey("right")) // move right 
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if (isFree())
                animator.Play("Player_run");

            // Change character position 
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey("a") || Input.GetKey("left")) // move left 
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

            if (isFree())
                animator.Play("Player_run");

            // Change character position
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // no horizontal movement 
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            if (isFree())
                animator.Play("Player_idle");
        }

        if (Input.GetKey("space") && isGrounded) // jump
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.Play("Player_jump");
        }
    }

    private bool isFree()
    {
        if (isGrounded && !isAttacking && !isCrouched)
        {
            return true;
        }

        return false;
    }

}