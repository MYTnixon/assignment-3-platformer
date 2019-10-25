using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerController playerController;

    public int total;

    // Start is called before the first frame update
    void Start()
    {
        playerController = playerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
