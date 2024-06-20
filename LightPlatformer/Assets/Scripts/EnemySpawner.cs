using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    public GameObject enemy;

    private void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string colliderTag = collision.gameObject.tag;
        switch (colliderTag)
        {
            case "Player":
                Debug.Log(spawnPoint.position);
                Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
                break;
        }
    }
}
