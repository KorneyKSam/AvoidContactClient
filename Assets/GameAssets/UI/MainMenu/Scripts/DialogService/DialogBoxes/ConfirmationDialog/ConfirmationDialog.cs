using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogBoxService
{
    public class ConfirmationDialog : MonoBehaviour, IDialogBox
    {
        public Transform Transform => transform;

        [SerializeField]
        private TMP_Text m_Title;

        [SerializeField]
        private TMP_Text m_Message;

        [SerializeField]
        private TMP_Text m_ConfirmText;

        [SerializeField]
        private TMP_Text m_CancelText;

        [SerializeField]
        private Button m_ConfirmButton;

        [SerializeField]
        private Button m_CancelButton;

        private Action<bool> m_ResultCallback;

        public void SetInfo(ConfirmationDialogInfo confirmationPopupInfo, Action<bool> resultCallback = null)
        {
            m_Title.text = confirmationPopupInfo.Title;
            m_Message.text = confirmationPopupInfo.Message;
            m_ConfirmText.text = confirmationPopupInfo.ConfirmText;
            m_CancelText.text = confirmationPopupInfo.CancelText;
            m_ResultCallback = resultCallback;
        }

        public void ActivateCancelButton(bool isActive)
        {
            m_CancelButton.gameObject.SetActive(isActive);
        }

        private void OnConfirm()
        {
            m_ResultCallback?.Invoke(true);
        }

        private void OnCancel()
        {
            m_ResultCallback?.Invoke(false);
        }

        private void Awake()
        {
            m_ConfirmButton.onClick.AddListener(OnConfirm);
            m_CancelButton.onClick.AddListener(OnCancel);
        }

        private void OnDestroy()
        {
            m_ConfirmButton.onClick.RemoveListener(OnConfirm);
            m_CancelButton.onClick.RemoveListener(OnCancel);
        }
    }
}