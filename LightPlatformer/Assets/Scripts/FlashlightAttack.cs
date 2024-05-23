using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class FlashlightAttack : MonoBehaviour
{
    [SerializeField] Light2D Light;
    public float normalIntensity;
    private bool attacked = false;

    private List<GameObject> enemies = new List<GameObject>();

    public LayerMask enemyLayer;

    public float attackCooldown = 3f;
    private float cooldownTimer = 0f;
    private float cooldownPercentage = 1f;
    private bool canAttack = true;

    void Start()
    {
        normalIntensity = Light.intensity;
    }

    void Update()
    {
        if (attacked)
        {
            FlashlightFlash();
        }

        //Attack cooldown
        if (!canAttack)
        {
            cooldownTimer += Time.deltaTime;
            cooldownPercentage = cooldownTimer/attackCooldown;

            Light.intensity = normalIntensity * cooldownPercentage;
            if (cooldownTimer >= attackCooldown)
            {
                canAttack = true;
                cooldownPercentage = 1f;
                cooldownTimer = 0f;
            }
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            #region Delete enemies
            if (canAttack)
            {
                canAttack = false;
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
            #endregion
        }
    }

    void DestroyEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    private void FlashlightFlash()
    {
        
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
