using UnityEngine;
using DG.Tweening;
using UI;
using UnityEngine.UI;

public class MainMenuUIPanel : Panel
{
    public Button playButton;
    public Button quitButton;

    public override void OnAwake()
    {
        playButton.onClick.AddListener(OnPlayButton);
        quitButton.onClick.AddListener(OnQuitButton);
    }

    private void OnPlayButton()
    {
        GameManager.Instance.StartGame();
    }

    private void OnQuitButton()
    {
        GameManager.Instance.QuitGame();
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
}