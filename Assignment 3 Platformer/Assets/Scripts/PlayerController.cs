using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;
    bool isGrounded;

    [SerializeField]
    Transform platformCheck;


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
            rbody.velocity = new Vector2(8, rbody.velocity.y);
            //animator.Play("Player_Run");
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey("a"))
        {
            rbody.velocity = new Vector2(-8, rbody.velocity.y);
            //animator.Play("Player_Run");
            spriteRenderer.flipX = true;
        }
        else
        {
            //animator.Play("Player_Idle");
            rbody.velocity = new Vector2(0, rbody.velocity.y);
        }

        if (Input.GetKey("space") && isGrounded)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, 3);
            animator.Play("Player_Jump");
        }
    }
}
