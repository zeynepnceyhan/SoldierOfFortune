using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class GameUiPanel : Panel
    {
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
}