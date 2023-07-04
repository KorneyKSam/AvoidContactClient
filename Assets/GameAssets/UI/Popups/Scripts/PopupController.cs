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

        public void ShowConfirmationPopup(ConfirmationPopupType confirmationPopupType, bool onlyConfirmButton = false, Action<bool> resultCallback = null)
        {

        }
    }
}