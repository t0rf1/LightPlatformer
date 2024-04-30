using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyMovement : MonoBehaviour
{
    private BoxCollider2D collider2d;
    private Rigidbody2D rigBody2d;
    public LayerMask groundLayer;

    [SerializeField] Transform playerPosition;

    [SerializeField] float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;

    private int direction;


    void Start()
    {
        rigBody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        SetDirection();
        rigBody2d.velocity = new Vector2(direction * movementSpeed, rigBody2d.velocity.y);

        if (CheckWall())
        {
            Jump();
        }
    }

    private void SetDirection()
    {
        if (playerPosition.position.x >= transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    private bool CheckWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 3f, groundLayer);
        if (hit.collider != null)
        {
            return true;
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
