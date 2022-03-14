using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 750f;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource jumpAudio;

    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private AnimationState animationState;
    private float moveHorizontal = 0f;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        EventManager.onJump += IsPlayerJumpAudio;
    }

    private void OnDestroy()
    {
        EventManager.onJump -= IsPlayerJumpAudio;
    }

    public void IsPlayerJumpAudio()
    {
        jumpAudio.Play();
        rb.AddForce(new Vector2(0f, jumpForce));
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump") && CheckGround())
        {
            //jumpAudio.Play();
            //rb.AddForce(new Vector2(0f, jumpForce));
            EventManager.PlayerJump();
            
        }

        AnimationController();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = transform.right * Mathf.Abs(moveHorizontal);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, moveSpeed * Time.fixedDeltaTime);

        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    private bool CheckGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, whatIsGround);
    }

    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void AnimationController()
    {
        if (moveHorizontal > 0)
        {
            animationState = AnimationState.running;
        }
        else if (moveHorizontal < 0)
        {
            animationState = AnimationState.running;
        }
        else
        {
            animationState = AnimationState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            animationState = AnimationState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            animationState = AnimationState.falling;
        }

        animator.SetInteger("State", (int)animationState);
    }
}
