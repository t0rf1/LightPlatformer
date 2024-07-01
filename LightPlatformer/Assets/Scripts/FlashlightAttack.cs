using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightAttack : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    RandomAudioPlayer AudioScript;

    public LayerMask enemyLayer;

    [SerializeField] Animator animator;

    public bool canAttack;

    DieStopper dieStopper;

    GameObject player_obj;

    void Start()
    {
        canAttack = true;
        AudioScript = GetComponent<RandomAudioPlayer>();
        dieStopper = GetComponent<DieStopper>();
        player_obj = GameObject.Find("Player");
    }

    void Update()
    {
        if(canAttack && dieStopper.canMove)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canAttack)
            {

                player_obj.GetComponent<PlayerMovement>().Vibrator(.5f, .5f, .1f);

                AudioScript.PlayRandomSound(AudioScript.clipList1);
                animator.SetTrigger("Attacked");
                canAttack = false;
                DeleteEnemy();
            }
        }
    }

    void DeleteEnemy() //Whole logic of killing enemy
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

        //Duplicates list of enemies in range
        List<GameObject> enemiesToDestroy = new List<GameObject>(enemies);

        //For each enemy in range, checks if can be hit, then kills
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

    void DestroyEnemy(GameObject enemy) //Removes from list and destroys
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    void SetCanAttack()
    {
        canAttack = true;

        player_obj.GetComponent<PlayerMovement>().Vibrator(.3f, .3f, .2f);
    }
    void StartedRecharging()
    {
        AudioScript.PlayRandomSound(AudioScript.clipList2);
    }

    //Adds object to list on collision enter, removes on collision exit
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
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
