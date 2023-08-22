using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements.Buttons
{
    public class PasswordButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Customization")]
        [SerializeField]
        private Sprite m_ShowPasswordIcon;

        [SerializeField]
        private Sprite m_HidePasswordIcon;

        [SerializeField]
        private Color m_NormalColor;

        [SerializeField]
        private Color m_OnPointerEnterColor;

        [Header("References")]
        [SerializeField]
        private Button m_Button;

        [SerializeField]
        private Image m_ButtonImage;

        [Header("Input fields")]
        [SerializeField]
        private List<TMP_InputField> m_PasswordFields;

        private bool m_IsPasswordShowed;

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_ButtonImage.color = m_OnPointerEnterColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_ButtonImage.color = m_NormalColor;
        }

        private void Awake()
        {
            m_Button.onClick.AddListener(SwitchPasswordFields);
        }

        private void OnDestroy()
        {
            m_Button.onClick.RemoveListener(SwitchPasswordFields);
        }

        private void SwitchPasswordFields()
        {
            m_IsPasswordShowed = !m_IsPasswordShowed;
            m_ButtonImage.sprite = m_IsPasswordShowed ? m_HidePasswordIcon : m_ShowPasswordIcon;
            m_PasswordFields.ForEach(f =>
            {
                f.contentType = m_IsPasswordShowed ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
                f.textComponent.SetAllDirty();
            });

            m_Button.enabled = false;
            m_Button.enabled = true;
        }
    }
}