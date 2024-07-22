using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBItemCollector : MonoBehaviour
{
    public bool pwrStatus;
    public BBMovement BB;
    public Animator anim;
    [SerializeField] private AudioSource collectionSound;

    private void Start()
    {
        anim = BB.GetComponent<Animator>();
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUps"))
        {
            Destroy(collision.gameObject);
            pwrStatus = true;
            collectionSound.Play();
            anim.SetTrigger("PowerUp");
        }
    }
}
