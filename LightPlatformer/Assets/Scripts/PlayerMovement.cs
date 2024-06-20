using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Essentals
    private Rigidbody2D rigBody2d;
    private CapsuleCollider2D collider2d;

    //Movement
    public Transform spawnPoint;
    public LayerMask groundLayer;
    private float horizontal;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;

    //Taking damage
    [SerializeField] Slider healthSlider;
    float currentHP;
    public float maxHP = 5;
    public int takingDamage = 1;
    bool knockbackeffect = false;

    //Audio
    RandomAudioPlayer AudioScript;
    bool soundPlay = true;

    //Animation
    public Animator animator;


    void Start()
    {
        rigBody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CapsuleCollider2D>();
        AudioScript = GetComponent<RandomAudioPlayer>();

        currentHP = maxHP;
    }

    void Update()
    {
        if (!knockbackeffect) 
        {
            rigBody2d.velocity = new Vector2(horizontal * movementSpeed, rigBody2d.velocity.y);
        }

        if (horizontal != 0){
            if(soundPlay)
            {
                AudioScript.audioSrc.Play();
                soundPlay = false;
            }
        }
        else
        {
            AudioScript.audioSrc.Stop();
            soundPlay = true;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (horizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rigBody2d.velocity = new Vector2(rigBody2d.velocity.x, jumpForce);
        }
        if(context.canceled && rigBody2d.velocity.y > 0)
        {
            rigBody2d.velocity = new Vector2(rigBody2d.velocity.x, rigBody2d.velocity.y * .5f);
        }
    }

    private bool IsGrounded()
    {
        Vector2 originVector = new Vector2(collider2d.size.x - .1f, collider2d.size.y - .1f);
        return Physics2D.BoxCast(collider2d.bounds.center, originVector, 0f, Vector2.down, .1f, groundLayer);
    }

    private void TakeDamage(int damage, Vector2 direction)
    {
        currentHP -= damage;
        rigBody2d.AddForce(direction * 5, ForceMode2D.Impulse);
        knockbackeffect = true;

        if(currentHP <= 0)
        {
            Die();
        }

        UpdateSlider();
    }

    private void UpdateSlider()
    {
        healthSlider.value = currentHP / maxHP;
    }

    private Vector2 SetKnockbackDirection(GameObject enemy)
    {
        float distanceToEnemy = enemy.transform.position.x - transform.position.x;

        if (distanceToEnemy >= 0)
        {
            return new Vector2(-1, .7f);
        }
        else //if (distanceToEnemy <= 0)
        {
            return new Vector2(1, .7f);
        }
    }
    void Die()
    {
        Debug.Log("dies");
        GameManager.Instance.UpdateGameState(gameState.DeathScreen);
        Time.timeScale = 0;
    }

    //Collision on tags with triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string colliderTag = collision.gameObject.tag;
        switch (colliderTag)
        {
            //Respawn
            case "Hole":
                Die();
                break;

            //Add HP
            case "addHP":
                currentHP++;
                UpdateSlider();
                Destroy(collision.gameObject);
                break;
        }
    }

    //Collision on tags with colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderTag = collision.gameObject.tag;
        switch (colliderTag)
        {
            //Enemy contact
            case "Enemy":
                TakeDamage(takingDamage, SetKnockbackDirection(collision.gameObject));
                break;

            case "Ground":
                knockbackeffect = false;
                break;
        }
    }
}