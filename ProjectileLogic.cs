using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private BBItemCollector powerStatus;
    [SerializeField] private BBMovement dir;
    [SerializeField] private float despawnTime;


    void Start()
    {
        dir = GameObject.Find("BB_player").GetComponent<BBMovement>();
        powerStatus = GameObject.Find("BB_player").GetComponent<BBItemCollector>();
        ProjectileMovement();
    }

    void Update()
    {

    }

    void ProjectileMovement()
    {
        projectile.AddForce(Vector2.right * dir.direction * projectileSpeed, ForceMode2D.Impulse);
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
