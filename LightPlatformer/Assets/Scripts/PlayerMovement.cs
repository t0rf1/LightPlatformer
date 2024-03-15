using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigBody2d;
    private BoxCollider2D collider2d;

    public Transform spawnPoint;
    public LayerMask groundLayer;
    private float horizontal;

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;

    void Start()
    {
        rigBody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        rigBody2d.velocity = new Vector2(horizontal * movementSpeed, rigBody2d.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
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
        Vector2 originVector = new Vector2(collider2d.size.x, collider2d.size.y);
        return Physics2D.BoxCast(collider2d.bounds.center, originVector, 0f, Vector2.down, .1f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string colliderTag = collision.gameObject.tag;
        switch (colliderTag)
        {
            case "Hole":
                rigBody2d.MovePosition(spawnPoint.position);
                break;
        }
    }
}
