using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class BBMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;
    [SerializeField] private BBItemCollector powerStatus;

    [SerializeField] private LayerMask jumpableGround;
    public float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpSpeed = 14f;
    [SerializeField] private float coyoteTime;
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime;
    private float jumpBufferCounter;

    [SerializeField] private GameObject BBball;
    [SerializeField] private float shootHeight;
    public float direction;
    
    [SerializeField] private AudioSource jumpSound;

    public bool isKnocked;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackTime;
    public bool knockFromRight;

    public PauseLogic paused;

    


    private enum MovementState { idle, running, jumping, falling, sliding, powerIdle, powerRun, powerJump, powerFall, powerSlide }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        direction = 1f;
        isKnocked = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused.isPause)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            if (dirX < 0f)
            {
                direction = -1f;
            }
            else if (dirX > 0f)
            {
                direction = 1f;
            }

            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
        

        if (isKnocked)
        {
            StartCoroutine(KnockBack());
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && !paused.isPause & IsGrounded())
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpBufferCounter = jumpBufferTime;
            coyoteTimeCounter = 0f;
        }
        else if (Input.GetButtonDown("Jump") && !paused.isPause & !IsGrounded())
        {
            jumpBufferCounter = jumpBufferTime;
            if (coyoteTimeCounter > 0f)
            {
                jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                coyoteTimeCounter = 0f;
            }
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            jumpBufferCounter = 0f;
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            coyoteTimeCounter = 0f;
        }

        if (powerStatus.pwrStatus)
        {
            coll.size = new Vector2(0.950007f, 2.118617f);
            coll.offset = new Vector2(-0.01201367f, -0.6766953f);
        }
        else
        {
            coll.size = new Vector2(0.7097292f, 1.750076f);
            coll.offset = new Vector2(0f, -0.4424247f);
        }


        if (Input.GetKeyDown(KeyCode.E) && powerStatus.pwrStatus && !paused.isPause)
        {
            anim.SetTrigger("shooting");
            Instantiate(BBball, transform.position + new Vector3(direction, shootHeight, transform.position.z), BBball.transform.rotation);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f && !powerStatus.pwrStatus)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f && !powerStatus.pwrStatus)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f && !powerStatus.pwrStatus)
        {
            state = MovementState.jumping;
        }

        else if (rb.velocity.y < -.1f && !powerStatus.pwrStatus)
        {
            state = MovementState.falling;
        }


        if (dirX > 0f && powerStatus.pwrStatus)
        {
            state = MovementState.powerRun;
            sprite.flipX = false;
        }
        else if (dirX < 0f && powerStatus.pwrStatus)
        {
            state = MovementState.powerRun;
            sprite.flipX = true;
        }
        else if (powerStatus.pwrStatus)
        {
            state = MovementState.powerIdle;
        }

        if (rb.velocity.y > .1f && powerStatus.pwrStatus)
        {
            state = MovementState.powerJump;
        }

        else if (rb.velocity.y < -.1f && powerStatus.pwrStatus)
        {
            state = MovementState.powerFall;
        }



        anim.SetInteger("state", (int)state);
    }
        
    IEnumerator KnockBack()
    {
        if (knockFromRight)
        {
            rb.velocity = new Vector2(-knockbackForce, knockbackForce);
        }
        else if (!knockFromRight) 
        { 
            rb.velocity = new Vector2(knockbackForce, knockbackForce); 
        }
        yield return new WaitForSeconds(0.3f);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        isKnocked = false;
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    
}
