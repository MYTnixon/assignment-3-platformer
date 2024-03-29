﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private float distance;
    public static Rigidbody2D rbodyEnemy;
    private bool movingRight = true;
    public Transform enemyPlatformCheck;
    public static int enemiesKilled;

    void Start()
    {
        rbodyEnemy = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D platform = Physics2D.Raycast(enemyPlatformCheck.position, Vector2.down, distance);
        if (platform.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
