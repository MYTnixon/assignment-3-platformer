﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;
    public bool isGrounded;

    public float speed;
    public float jumpHeight;

    public Transform platformCheck;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, platformCheck.position, 1 << LayerMask.NameToLayer("Platforms")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKey("d"))
        {
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
            //if (isGrounded)
            //animator.Play("Player_Run");
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey("a"))
        {
            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            //if (isGrounded)
            //animator.Play("Player_Run");
            spriteRenderer.flipX = true;
        }
        else
        {
            if (isGrounded)
            animator.Play("Player_Idle");
            rbody.velocity = new Vector2(0, rbody.velocity.y);
        }

        if (Input.GetKey("space") && isGrounded)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight);
            animator.Play("Player_Jump");
        }
    }
}
