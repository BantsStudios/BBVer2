using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    public void SpawnEnemy()
    {
        Instantiate(enemy);
    }
}
