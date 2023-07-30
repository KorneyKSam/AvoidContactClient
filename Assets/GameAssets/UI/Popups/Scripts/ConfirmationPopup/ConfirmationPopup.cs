using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class ConfirmationPopup : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_Title;

        [SerializeField]
        private TMP_Text m_Message;

        [SerializeField]
        private TMP_Text m_ConfirmText;

        [SerializeField]
        private TMP_Text m_CancelText;

        [SerializeField]
        private Button m_CancelButton;

        private Action<bool> m_Callback;

        public void SetInfo(ConfirmationPopupInfo confirmationPopupInfo, Action<bool> callback)
        {
            m_Title.text = confirmationPopupInfo.Title;
            m_Message.text = confirmationPopupInfo.Message;
            m_ConfirmText.text = confirmationPopupInfo.ConfirmText;
            m_CancelText.text = confirmationPopupInfo.CancelText;
            m_Callback = callback;
        }

        public void ActivateCancelButton(bool isActive)
        {
            m_CancelButton.gameObject.SetActive(isActive);
        }

        public void OnConfirm()
        {
            m_Callback?.Invoke(true);
        }

        public void OnCancel()
        {
            m_Callback?.Invoke(false);
        }

        public void ShowPopup()
        {
            gameObject.SetActive(true);
        }

        public void HidePopup()
        {
            gameObject.SetActive(false);
        }
    }
}