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
        private Button m_ConfirmBtn;

        [SerializeField]
        private Button m_CancelBtn;

        public void SetInfo(ConfirmationPopupInfo confirmationPopupInfo)
        {

        }

        public void ShowPopup()
        {

        }

        public void HidePopup()
        {

        }
    }
}