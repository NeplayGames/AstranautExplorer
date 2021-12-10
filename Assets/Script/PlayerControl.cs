using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 10f;


    Rigidbody2D rb;

    float middleWidth;

    [SerializeField] ScoreCounter scoreCounter;
    Animator animator;

    bool canJump = true;

    bool playerDeath = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        middleWidth = Screen.width / 2;
    }

    Vector3 initialPoint;

    private void Update()
    {
        if(playerDeath) return;
        if (Input.GetMouseButtonDown(0))
        {
            initialPoint = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            
            Vector2 touchPoint = Input.mousePosition;
          
                MovementSideways(touchPoint);
            
            if(touchPoint.y - initialPoint.y >= 100 && canJump)
            {
                canJump = false;
                Jump();

            }

        }
        else
        {
            //  if(canJump)
            //  rb.velocity = Vector2.zero;
            ResetAnimation();
        }
    }

    /// <summary>
    /// This is used to reset the animation to get to idle state
    /// </summary>
    private void ResetAnimation()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Jump", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Ground"))
        {
            canJump = true;
            initialPoint = Input.mousePosition;

        }

        if (collider.CompareTag("Spike"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        playerDeath = true;
        animator.SetBool("Dead", true);

        scoreCounter.GameOver("You cannot walk on spikes.");
    }

    private void Jump()
    {
       rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        animator.SetBool("Jump", true);

    }

    private void MovementSideways(Vector2 touchPoint)
    {
        if (touchPoint.x > middleWidth)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector2(0, 180));

        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        }
        animator.SetBool("Run", true);
        animator.SetBool("Jump", false);
    }
}
