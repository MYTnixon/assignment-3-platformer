using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int coins;
    public int enemies;
    public int deaths;
    public int scoreTotal;
    public bool restarted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coins = CoinScript.coinsCollected;
        enemies = EnemyController.enemiesKilled;
        deaths = PlayerController.deathCount;
        restarted = RestartButton.isRestarted;
        scoreTotal = coins + enemies + deaths;

        if (scoreTotal <= -1)
        {
            scoreTotal = 0;
        }

        if (restarted == true)
        {
            coins = 0;
            enemies = 0;
            deaths = 0;
            restarted = false;
        }
    }
}
