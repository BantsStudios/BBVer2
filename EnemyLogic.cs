using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private float enemySpeed;
    private float enemyDirection;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask jumpableGround;
    private BoxCollider2D coll;
    [SerializeField] private BBMovement BBDir;
    [SerializeField] private BBMovement BB;


    [SerializeField] private string enemyType;
    [SerializeField] private AudioSource dyingSound;



    void Start()
    {
        enemySpeed = 0f;
        enemyDirection = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        if (BB.transform.position.x > transform.position.x)
        {
            enemyDirection = 1f;
            sprite.flipX = true;
        }
        else if (BB.transform.position.x <  transform.position.x) 
        { 
            enemyDirection = -1f;
            sprite.flipX = false;
        }
        rb.velocity = new Vector2(enemyDirection * enemySpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BB"))
        {
            ActivateEnemy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BBball") || collision.gameObject.CompareTag("Traps"))
        {
            dyingSound.Play();
            StartCoroutine(Die());

        }
    }

    private void ActivateEnemy()
    {
        enemySpeed = 3f;
    }

    IEnumerator Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        if (enemyType  == "burger")
        {
            anim.SetTrigger("BurgerDie");
        }
        else if (enemyType == "broccoli")
        {
            anim.SetTrigger("BroccoliDeath");
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
