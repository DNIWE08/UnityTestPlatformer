using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsFollower : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] waypoints;
    private int currPointIndex = 0;
    private AnimationState animationState;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private int m_direction;

    [SerializeField] private float speed = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Vector2.Distance(waypoints[currPointIndex].transform.position, transform.position) < 0.3f)
        {
            currPointIndex++;
            if(currPointIndex >= waypoints.Length)
            {
                currPointIndex = 0;
            }
        }

        if(waypoints[currPointIndex].transform.position.x > transform.position.x)
        {
            m_direction = 1;
        }
        else
        {
            m_direction = -1;
        }
        AnimationController();
    }

    private void FixedUpdate()
    {
        MovementController(m_direction);
    }

    private void MovementController(int direction)
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        if(direction > 0)
        {
            sprite.flipX = true;
        } 
        else
        {
            sprite.flipX = false;
        }
    }

    private void AnimationController()
    {
        if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            animationState = AnimationState.running;
        } 
        else
        {
            animationState = AnimationState.idle;
        }
        animator.SetInteger("State", (int)animationState);
    }
}
