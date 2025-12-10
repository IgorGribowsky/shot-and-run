using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public string[] Levels;

    public int CurrentLevel = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetFirstLevel()
    {
        CurrentLevel = 0;
        LoadCurrentLevel();
    }

    public void SetNextLevel()
    {
        if (CurrentLevel < Levels.Length)
        {
            CurrentLevel++;
            LoadCurrentLevel();
        }
        else
        {
            Debug.Log("Последний уровень пройден");
        }
    }

    public void LoadCurrentLevel()
    {
        if (CurrentLevel == 0)
        {
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            SceneManager.LoadScene(Levels[CurrentLevel-1]);
        }
    }
}