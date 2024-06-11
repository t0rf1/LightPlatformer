using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyMovement : MonoBehaviour
{
    private CapsuleCollider2D collider2d;
    private Rigidbody2D rigBody2d;
    public LayerMask groundLayer;

    [SerializeField] Transform playerPosition;

    [SerializeField] float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float checkWallDistanceJump = 1.8f;
    [SerializeField] private float overlapDistanceStop = .2f;

    private float distanceToPlayer;

    private int direction;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rigBody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        distanceToPlayer = playerPosition.position.x - transform.position.x;
        SetDirection();

        //move
        rigBody2d.velocity = new Vector2(direction * movementSpeed, rigBody2d.velocity.y);

        if (rigBody2d.velocity.x < 0)
        {
            spriteRenderer.flipX = true;

        }
        else if (rigBody2d.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }



        //if wall is detected, jump
        if (CheckWall(direction, checkWallDistanceJump))
        {
            Jump();
        }
    }

    private void SetDirection()
    {
        if (distanceToPlayer >= 0 + overlapDistanceStop)
        {
            direction = 1;
        }
        else if (distanceToPlayer <= 0 - overlapDistanceStop)
        {
            direction = -1;
            
        }
        else 
        { 
            direction = 0; 
        }
    }

    private bool CheckWall(int direction, float checkWallDistance)
    {
        if(direction >= 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, checkWallDistance, groundLayer);
            if (hit.collider != null)
            {
                return true;
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, checkWallDistance, groundLayer);
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rigBody2d.velocity = new Vector2(rigBody2d.velocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        Vector2 sizeVector = new Vector2(collider2d.size.x, collider2d.size.y);
        return Physics2D.BoxCast(collider2d.bounds.center, sizeVector, 0f, Vector2.down, .1f, groundLayer);
    }
}
