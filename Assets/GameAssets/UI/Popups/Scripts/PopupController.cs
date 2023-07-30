using System;
using UnityEngine;

namespace UI.Popups
{
    public class PopupController : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_PopupsContainer;

        [SerializeField]
        private GameObject m_FadeOverlay;

        [SerializeField]
        private ConfirmationPopup m_ConfirmationPopup;

        public void ShowConfirmationPopup(ConfirmationPopupType confirmationPopupType, Action<bool> resultCallback = null, bool onlyConfirmButton = false)
        {
            m_FadeOverlay.SetActive(true);
            m_ConfirmationPopup.SetInfo(GetInfoByKey(confirmationPopupType), (isConfirmed) =>
            {
                m_FadeOverlay.SetActive(false);
                m_ConfirmationPopup.HidePopup();
                resultCallback?.Invoke(isConfirmed);
            });
            m_ConfirmationPopup.ActivateCancelButton(!onlyConfirmButton);
            m_ConfirmationPopup.ShowPopup();
        }

        private static ConfirmationPopupInfo GetInfoByKey(ConfirmationPopupType confirmationPopupType)
        {
            //TODO:Loading localization by key and launguage tag

            return new ConfirmationPopupInfo()
            {
                Title = "Кто ты по жизни",
                Message = "Ответишь, ты кто по жизни?",
                ConfirmText = "Отвечу!",
                CancelText = "Я меньжуюсь!",
            };
        }
    }
}