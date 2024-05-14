using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    private Rigidbody2D rigBody2d;
    Animator animator;
    bool isFacingRight = true;
    bool isJumping;

    void Start()
    {
        rigBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        //flip direction
        if (rigBody2d.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (rigBody2d.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }

        //animation
        animator.SetFloat("Speed", Mathf.Abs(rigBody2d.velocity.x));

        //jumping
        //if (isJumping == false && Input.GetKeyDown(KeyCode.Space))
        //{
        //    isJumping = true;
        //    animator.SetBool("IsJumping", true);
        //}
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
