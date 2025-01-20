using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UI;

public class UIManager : MonoBehaviour
{
    public List<PanelData> panelDatas;
    public static UIManager Instance;
    public Text scoreText;

    protected virtual void Awake()
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

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }


    public void ShowPanel(GameManager.GameState newState)
    {
        HideAllPanels(newState);

        var newPanelData = panelDatas.FirstOrDefault(x => x.state == newState);
        var panel = newPanelData.panel;

        panel.Show();
    }

    public void HideAllPanels(GameManager.GameState newState)
    {
        foreach (var paneldata in panelDatas)
        {
            var panel = paneldata.panel;
            if (paneldata.state != newState)
            {
                panel.Hide();
            }
        }
    }
}

[Serializable]
public class PanelData
{
    public GameManager.GameState state;
    public Panel panel;
}