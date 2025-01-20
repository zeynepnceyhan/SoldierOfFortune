using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLimit = 60f;
    private float timeRemaining;
    private bool timerActive;

    void Start()
    {
        timeRemaining = timeLimit;
        GameManager.Instance.OnGameStateChanged += OnGameStateChange;
    }


    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChange;
    }

    private void OnGameStateChange(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Playing)
        {
            timerActive = true;
        }
        else
        {
            timerActive = false;
        }
    }

    void Update()
    {
        if (timerActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                TimerFinished();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        var minutes = Mathf.FloorToInt(timeToDisplay / 60);
        var seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerFinished()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            return;
        }

        timerText.text = "TIME'S UP!";
        Debug.Log("Timer Finished. Changing state to YouWin.");
        GameManager.Instance.ChangeState(GameManager.GameState.YouWin);
    }
}