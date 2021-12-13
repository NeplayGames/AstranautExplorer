using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    Rigidbody2D rb;
    [SerializeField] ScoreCounter scoreCounter;
    Animator animator;
    bool canJump = true;
    bool playerDeath = false;
    float horizontal;
    //This is used to determine if the button is pressed
    [SerializeField] GameObject controls;

    [DllImport("__Internal")]
    private static extern bool IsMobile();
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        TouchControls.OnPress += TouchControls_OnPress;
        if (!isMobile())
        {
            controls.SetActive(false);
        }   
    }
     bool isMobile()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
             return IsMobile();
        #endif
        return false;
    }
    private void OnDestroy()
    {
        TouchControls.OnPress -= TouchControls_OnPress;
    }
    private void TouchControls_OnPress(int obj)
    {
         MovementSideways(obj);   
    }

    private void Update()
    {
        if(playerDeath) return;
        if (!isMobile())
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            MovementSideways(Input.GetAxis("Horizontal"));
        }  
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    /// <summary>
    /// This is used to reset the animation to get to idle state
    /// </summary>
    private void ResetAnimation()
    {
        animator.SetBool("Run", false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerDeath) return;
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Ground"))
        {
            canJump = true;
        }
        if (collider.CompareTag("Spike"))
        {
            GameOver();
        }
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            scoreCounter.GameWon();
            animator.SetBool("Won", true);
            playerDeath = true;
        }
    }

    private void GameOver()
    {
        playerDeath = true;
        animator.SetBool("Dead", true);
        scoreCounter.GameOver("You cannot walk on spikes.");
    }

    public void Jump()
    {
        if ( canJump)
        {
            canJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
    private void MovementSideways(float horizontal)
    {
        this.horizontal = horizontal;
        if (horizontal == 0) { ResetAnimation(); return; }
            transform.rotation = Quaternion.Euler(new Vector2(0,horizontal < 0 ? 0: 180));
        animator.SetBool("Run", true);
        return;
    }
}
