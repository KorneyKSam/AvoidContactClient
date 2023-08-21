using MVVM;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace UI.ViewModels
{
    [Binding]
    public class AuthorizationViewModel : BaseMonoPropertyChanged, INetworkConnectionViewModel
    {
        public Button AuthorizationButton => m_AuthorizationBtn;
        public Button CancelButton => m_CancelBtn;
        public Button TransitToRegistrationBtn => m_NoAccountBtn;
        public Button ResetPasswordBtn => m_ForgotPasswordBtn;

        [Binding]
        public string Login
        {
            get => m_Login;
            set => Set(ref m_Login, value);
        }

        [Binding]
        public string Password
        {
            get => m_Password;
            set => Set(ref m_Password, value);
        }

        [Binding]
        public bool IsAutomaticAuthorization
        {
            get => m_IsAutomaticAuthorization;
            set => Set(ref m_IsAutomaticAuthorization, value);
        }

        [Binding]
        public string TooltipMessage
        {
            get => m_TooltipMessage;
            set => Set(ref m_TooltipMessage, value);
        }

        [Binding]
        public bool IsConnected
        {
            get => m_IsConnected;
            set
            {
                if (Set(ref m_IsConnected, value))
                {
                    OnPropertyChanged(nameof(IsAuthorizeButtonInteractable));
                }
            }
        }

        [Binding]
        public bool IsAuthorized
        {
            get => m_IsAuthorized;
            set
            {
                if (Set(ref m_IsAuthorized, value))
                {
                    OnPropertyChanged(nameof(IsAuthorizeButtonInteractable));
                }
            }
        }

        [Binding]
        public bool IsAuthorizeButtonInteractable
        {
            get => m_IsConnected && !IsAuthorized;
        }

        [Header("Buttons")]
        [SerializeField]
        private Button m_AuthorizationBtn;

        [SerializeField]
        private Button m_CancelBtn;

        [SerializeField]
        private Button m_NoAccountBtn;

        [SerializeField]
        private Button m_ForgotPasswordBtn;

        private string m_Login;
        private string m_Password;
        private string m_TooltipMessage;
        private bool m_IsConnected;
        private bool m_IsAuthorized;
        private bool m_IsAutomaticAuthorization;
    }
}