using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int CharacterIdx;
    public SkillSO playerSkillData;
    public string playerName;
    public string playerWeaponName;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Loading
    }

    private GameState currentGameState;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        //SetGameState(GameState.MainMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.Playing)
        {
            TogglePause();
        }
    }

    public void SetGameState(GameState newState)
    {
        currentGameState = newState;

        switch (currentGameState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene("MainMenu");
                Time.timeScale = 1f;
                break;
            case GameState.Playing:
                SceneManager.LoadScene("GamePlayScene");
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.Loading:
                SceneManager.LoadScene("LoadingScene");
                Time.timeScale = 1f;
                break;
            case GameState.GameOver:
                SceneManager.LoadScene("GameOverScene");
                Time.timeScale = 1f;
                break;
            default:
                break;
        }
    }

    private void TogglePause()
    {
        if (currentGameState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
        }
        else if (currentGameState == GameState.Paused)
        {
            SetGameState(GameState.Playing);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
