using System;
using UnityEngine;
using UnityEngine.UI;

namespace DialogBoxService
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public abstract class BaseDialog : MonoBehaviour, IDialogBox
    {
        public bool IsActive => gameObject.activeInHierarchy;

        public DialogLayer SortingLayer
        {
            get => m_SortingLayer;
            set
            {
                m_SortingLayer = value;
                m_Canvas.overrideSorting = true;
                m_Canvas.sortingLayerName = m_SortingLayer.ToString();
            }
        }

        private DialogLayer m_SortingLayer;
        private Canvas m_Canvas;

        public abstract void Activate(bool isActive, float duration, Action onCompleteAnimation = null);

        private void OnValidate()
        {
            m_Canvas = GetComponent<Canvas>();
        }
    }
}