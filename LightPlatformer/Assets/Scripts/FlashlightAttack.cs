using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightAttack : MonoBehaviour
{
    [SerializeField] GameObject light;

    private List<GameObject> enemies = new List<GameObject>();

    public LayerMask enemyLayer;

    void Start()
    {
    }

    void Update()
    {
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Sort list by enemy distance to player
            enemies.Sort(
                delegate (GameObject enemy1, GameObject enemy2)
                {
                    float distance1 = (enemy1.transform.position - transform.position).magnitude;
                    float distance2 = (enemy2.transform.position - transform.position).magnitude;
                    return distance1.CompareTo(distance2);
                }
            );

          
            List<GameObject> enemiesToDestroy = new List<GameObject>(enemies);
            foreach (GameObject enemy in enemiesToDestroy)
            {
                Vector2 direction = enemy.transform.position - transform.position;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, Mathf.Infinity, enemyLayer);
                if (hit.collider.gameObject == enemy)
                {
                    DestroyEnemy(enemy);
                }
            }
        }
    }

    void DestroyEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    //Adds object to list on collision enter, removes on collision exit
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemies.Add(collision.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject);
        }
    }
}
