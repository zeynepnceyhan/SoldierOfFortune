using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public abstract class Panel : MonoBehaviour
    {
        protected RectTransform rectTransform;

        protected Tween ShowTween, HideTween;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            OnAwake();
        }

        [Button]
        public void KillTweens()
        {
            ShowTween?.Kill(true);
            HideTween?.Kill(true);
        }

        [Button]
        public abstract void Show();

        [Button]
        public abstract void Hide();

        public virtual void OnAwake()
        {
        }
    }
}