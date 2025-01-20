using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text highScoreText; 

    private void Start()
    {
        UpdateHighScoreText();
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", 1); 
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void UpdateHighScoreText()
    {
        int maxZombieKills = DataManager.LoadMaxZombieKills(); 
        highScoreText.text = "Max Zombies Killed: " + maxZombieKills; 
    }
}