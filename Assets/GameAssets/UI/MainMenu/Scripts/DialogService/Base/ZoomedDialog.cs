using DG.Tweening;
using System;
using UnityEngine;

namespace DialogBoxService
{
    public abstract class ZoomedDialog : BaseDialog
    {
        public override void Activate(bool isActive, float duration, Action onCompleteAnimation = null)
        {
            transform.gameObject.SetActive(true);
            Vector2 endValue = isActive ? Vector2.one : Vector2.zero;
            transform.DOScale(endValue, duration).OnComplete(() =>
            {
                transform.gameObject.SetActive(isActive);
                onCompleteAnimation?.Invoke();
            });
        }
    }
}