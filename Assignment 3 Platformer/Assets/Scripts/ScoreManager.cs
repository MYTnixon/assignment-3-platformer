using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int coins;
    public int enemies;
    public int scoreTotal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coins = CoinScript.coinsCollected;
        enemies = EnemyController.enemiesKilled;
        scoreTotal = coins + enemies;
    }
}
