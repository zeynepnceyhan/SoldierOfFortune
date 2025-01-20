using UnityEngine;
using DG.Tweening;
using UI;
using UnityEngine.UI;

public class GameWonUIPanel : Panel
{
    
    public Button nextLevelButton;
    public Button mainMenuButton;
    
    public override void OnAwake()
    {
        nextLevelButton.onClick.AddListener(OnNextLevelButton);
        mainMenuButton.onClick.AddListener(OnMainMenuButton);
    }

    private void OnNextLevelButton()
    {
        GameManager.Instance.LoadNextLevel();
    }
    private void OnMainMenuButton()
    {
        GameManager.Instance.GoToMainMenu();
    }
    
    public override void Show()
    {
        KillTweens();
        ShowTween = rectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutBounce);
    }

    public override void Hide()
    {
        KillTweens();
        HideTween=rectTransform.DOAnchorPos(Vector2.up * 3000f, 0.5f).SetEase(Ease.OutBounce);
    }

    public void OnNextLevelButtonPressed()
    {
        GameManager.Instance.LoadNextLevel();
    }
}