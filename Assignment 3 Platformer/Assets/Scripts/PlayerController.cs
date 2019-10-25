using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

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
    public int score;
    public int scoreTotal;
    private GameObject[] coins;
    private Vector2 startPos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coins = GameObject.FindGameObjectsWithTag("Coin");
        startPos = rbody.position;

        score = 0;
        UpdateCoinText();
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
        /*if (collision.gameObject.tag.Equals("DeathTrigger"))
        {
            gameObject.SetActive(false);
        }*/
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
            score += 1;
            UpdateCoinText();
        }
        else if (collision.gameObject.CompareTag("WinTrigger"))
        {
            scoreTotal += score;
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
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins: " + score;
    }
    void UpdateTotalScoreText()
    {
        totalScoreText.text = "Total Score: " + scoreTotal;
    }

    void resetGameState()
    {
        score = 0;
        UpdateCoinText();
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].gameObject.SetActive(true);
        }

        rbody.position = startPos;
        rbody.velocity = Vector2.zero;
        
    }

}
