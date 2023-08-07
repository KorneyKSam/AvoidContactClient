using DG.Tweening;
using GOOfTpeAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogBoxService
{
    public class DialogService : MonoBehaviour
    {
        public bool IsAnyDialogOpened => m_GODialogBoxes.Any(b => b.transform.gameObject.activeInHierarchy);

        private const float m_DefaultDuration = 0.4f;

        [SerializeField]
        private GameObject m_FadeOverlay;

        [SerializeField, GameObjectOfType(typeof(IDialogBox)), Tooltip("Game object has to implement " + nameof(IDialogBox))]
        private List<GameObject> m_GODialogBoxes;

        private List<IDialogBox> m_DialogBoxes = new();

        public T Open<T>(BoxAnimation boxAnimation = BoxAnimation.Zoom, float duration = m_DefaultDuration, Action onCompleteAnimation = null) where T : IDialogBox
        {
            return TryToUseDialog<T>(boxAnimation, duration, onCompleteAnimation, true);
        }

        public T Close<T>(BoxAnimation boxAnimation = BoxAnimation.Zoom, float duration = m_DefaultDuration, Action onCompleteAnimation = null) where T : IDialogBox
        {
            return TryToUseDialog<T>(boxAnimation, duration, onCompleteAnimation, false);
        }

        public T GetDialog<T>() where T : IDialogBox
        {
            return (T)GetDialogBox<T>();
        }

        private T TryToUseDialog<T>(BoxAnimation boxAnimation, float duration, Action onCompleteAnimation, bool isOpeningCommand) where T : IDialogBox
        {
            var dialogBox = GetDialogBox<T>();
            if (dialogBox != null)
            {
                m_FadeOverlay.SetActive(true);
                ShowAnimation(boxAnimation, duration, onCompleteAnimation, isOpeningCommand, dialogBox.Transform);
            }
            else
            {
                Debug.Log($"There is no dialog {typeof(T)} in list!!!"); //ToDo advanced dbugger
            }
            return (T)dialogBox;
        }

        private void ShowAnimation(BoxAnimation boxAnimation, float duration, Action onCompleteAnimation, bool isOpening, Transform transform)
        {
            switch (boxAnimation)
            {
                default:
                case BoxAnimation.Zoom:
                    transform.gameObject.SetActive(true);
                    m_FadeOverlay.SetActive(true);
                    Vector2 endValue = isOpening ? Vector2.one : Vector2.zero;
                    transform.DOScale(endValue, duration).OnComplete(() =>
                    {
                        transform.gameObject.SetActive(isOpening);
                        if (!isOpening && !IsAnyDialogOpened)
                        {
                            m_FadeOverlay.SetActive(false);
                        }
                        onCompleteAnimation?.Invoke();
                    });
                    break;
            }
        }

        private IDialogBox GetDialogBox<T>() where T : IDialogBox
        {
            return m_DialogBoxes.FirstOrDefault(b => b is T);
        }

        private void Awake()
        {
            if (m_GODialogBoxes != null)
            {
                m_GODialogBoxes.ForEach(b =>
                {
                    m_DialogBoxes.Add(b.GetComponent<IDialogBox>());
                    b.transform.localScale = Vector3.zero;
                });
            }
        }
    }
}