using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;
    public bool isGrounded;

    public float speed;
    public float jumpHeight;

    public Transform platformCheck;
    public Text coinText;
    public Text totalScoreText;

    public static int deathCount;
    
    
    private GameObject[] coins;
    private Vector2 startPos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coins = GameObject.FindGameObjectsWithTag("Coin");
        startPos = rbody.position;
    }

    private void Start()
    {
        ScoreManager.Instance.score = 0;
        UpdateCoinText();
        UpdateTotalScoreText();
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
            if (isGrounded)
            animator.Play("Player_Run");
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey("a"))
        {
            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            if (isGrounded)
            animator.Play("Player_Run");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            ScoreManager.Instance.AddScore(1);
            UpdateCoinText();
        }
        else if (collision.gameObject.CompareTag("WinTrigger"))
        {
            ScoreManager.Instance.scoreTotal += ScoreManager.Instance.score;
            UpdateTotalScoreText();
            int c = SceneManager.GetActiveScene().buildIndex;
            if (c < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(c + 1);
            resetGameState();
        }
        else if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            resetGameState();
        }
        else if (collision.gameObject.CompareTag("Button"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins: " + ScoreManager.Instance.score;
    }
    void UpdateTotalScoreText()
    {
        totalScoreText.text = "Total Coins: " + ScoreManager.Instance.scoreTotal;
    }

    void resetGameState()
    {
        ScoreManager.Instance.score = 0;
        UpdateCoinText();
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].gameObject.SetActive(true);
        }

        rbody.position = startPos;
        rbody.velocity = Vector2.zero;
    }
}
