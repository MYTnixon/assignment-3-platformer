using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public static bool isRestarted;

    public void restartScene()
    {
        SceneManager.LoadScene("SampleScene");
        isRestarted = true;
    }
}
