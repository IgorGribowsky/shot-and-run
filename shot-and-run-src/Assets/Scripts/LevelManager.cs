using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public string[] Levels;

    public int CurrentLevel { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        CurrentLevel = Array.IndexOf(Levels, SceneManager.GetActiveScene().name);
    }

    public void SetFirstLevel()
    {
        CurrentLevel = 0;
        LoadCurrentLevel();
    }

    public void SetNextLevel()
    {
        if (CurrentLevel < Levels.Length-1)
        {
            CurrentLevel++;
            LoadCurrentLevel();
        }
        else
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    public void LoadCurrentLevel()
    {
        if (CurrentLevel == -1)
        {
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            SceneManager.LoadScene(Levels[CurrentLevel]);
        }
    }
}