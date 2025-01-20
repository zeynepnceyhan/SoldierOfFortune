using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UI;

public class GameOverUIPanel : Panel
{
    public Text maxKillsText;
    
    public Button restartButton;
    public Button mainMenuButton;
    
    public override void OnAwake()
    {
       restartButton.onClick.AddListener(OnRestartButton);
       mainMenuButton.onClick.AddListener(OnMainMenuButton);
    }

    private void OnRestartButton()
    {
        GameManager.Instance.RestartLevel();
    }
    private void OnMainMenuButton()
    {
        GameManager.Instance.GoToMainMenu();
    }

    public override void Show()
    {
        KillTweens();
        ShowTween = rectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutBounce);

        var maxKills = DataManager.LoadMaxZombieKills();
        UpdateMaxKillsText(maxKills);
    }

    public override void Hide()
    {
        KillTweens();
        HideTween=rectTransform.DOAnchorPos(Vector2.up * 3000f, 0.5f).SetEase(Ease.OutBounce);
    }

    


    private void UpdateMaxKillsText(int maxKills)
    {
        if (maxKillsText != null)
        {
            maxKillsText.text = "Max Kills: " + maxKills.ToString();
        }
        else
        {
            Debug.LogError("MaxKillsText UI element is not assigned!");
        }
    }
}