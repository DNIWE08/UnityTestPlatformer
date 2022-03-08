using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 750f;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool grounded = false;
    private float moveHorizontal = 0f;
    private float groundedRadius = 0.2f;
    private bool facingRight = true;

    [Header("Events")]

    [Space]

    public UnityEvent OnLandEvent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        isJumping = false;
    }

    private void Move()
    {
        Vector3 dir = transform.right * moveHorizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, moveSpeed * Time.fixedDeltaTime);

        if (grounded && isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            grounded = false;
        }

        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!isJumping)
                {
                    OnLandEvent.Invoke();
                }
                
            }
        }
    }

    public void OnLanding ()
    {
        animator.SetBool("isJumping", false);
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
