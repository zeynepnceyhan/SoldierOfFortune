using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action<GameState> OnGameStateChanged; 
    public bool IsPlaying => CurrentState == GameState.Playing;
    
    public static GameManager Instance;
    public enum GameState { MainMenu, Playing, GameOver, YouWin }

    public GameState CurrentState;

    private int score;
    private int highScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        score = 0;
        highScore = DataManager.LoadMaxZombieKills(); 
        ChangeState(GameState.MainMenu);
    }

    public void StartGame()
    {
        score = 0;
        UIManager.Instance.UpdateScoreText(score); 
        ChangeState(GameState.Playing);
    }

    [Button]
    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState)
        {
            Debug.Log("State is already " + newState + ". No need to change.");
            return;
        }
        OnGameStateChanged.Invoke(newState);
        CurrentState = newState;
        Debug.Log("State changed to: " + newState);

        UIManager.Instance.ShowPanel(newState);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        Debug.Log($"Score updated: {score}"); 

        UIManager.Instance.UpdateScoreText(score); 

        if (score > highScore)
        {
            highScore = score;
            DataManager.SaveMaxZombieKills(highScore); 
        }
    }


    public void RestartLevel()
    {
        StartGame(); 
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void GoToMainMenu()
    {
        ChangeState(GameState.MainMenu);
    }
    
    public void LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            GoToMainMenu();
        }
    }

}
