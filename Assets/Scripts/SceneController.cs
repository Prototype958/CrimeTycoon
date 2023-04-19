using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void StartButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }
}
