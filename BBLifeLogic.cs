using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;
    private BoxCollider2D coll;
    [SerializeField] private BBItemCollector powerStatus;
    [SerializeField] private BBMovement BBDir;
    private SpriteRenderer sprite;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private BBMovement BB;
    [SerializeField] private Transform rightKnockCheck;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps") || collision.gameObject.CompareTag("Enemies"))
        {
            BB.isKnocked = true;

            if (!powerStatus.pwrStatus)
            {
                StartCoroutine(Die());
                StartCoroutine(RestartLevel());
            }

            else
            {
                if (collision.transform.position.x >= transform.position.x)
                {
                    BB.knockFromRight = true;
                }
                else
                {
                    BB.knockFromRight = false;
                }
                powerStatus.pwrStatus = false;
                anim.SetTrigger("PowerDown");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OutOfWorld"))
        {
            StartCoroutine(Die());
            StartCoroutine(RestartLevel());
        }
    }

    IEnumerator Die()
    {
        deathSound.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        yield return new WaitForSeconds(.5f);
        sprite.enabled = false;
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
