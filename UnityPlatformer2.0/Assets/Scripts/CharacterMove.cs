using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 20f;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool grounded = false;
    private float moveHorizontal = 0f;
    private float groundedRadius = 0.2f;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
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
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }
}
