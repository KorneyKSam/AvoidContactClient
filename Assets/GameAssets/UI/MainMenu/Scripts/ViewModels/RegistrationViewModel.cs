using MVVM;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace UI.ViewModels
{
    [Binding]
    public class RegistrationViewModel : BaseMonoPropertyChanged, INetworkConnectionViewModel
    {
        public Button RegistrationBtn => m_RegistrationBtn;
        public Button CancelRegistration => m_CancelRegistration;

        [Binding]
        public string Login
        {
            get => m_Login;
            set => Set(ref m_Login, value);
        }

        [Binding]
        public string Email
        {
            get => m_Email;
            set => Set(ref m_Email, value);
        }

        [Binding]
        public string Password
        {
            get => m_Password;
            set => Set(ref m_Password, value);
        }

        [Binding]
        public string RepeatedPassword
        {
            get => m_RepeatedPassword;
            set => Set(ref m_RepeatedPassword, value);
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
            set => Set(ref m_IsConnected, value);
        }

        [SerializeField]
        private Button m_RegistrationBtn;

        [SerializeField]
        private Button m_CancelRegistration;

        private string m_Login = string.Empty;
        private string m_Email = string.Empty;
        private string m_Password = string.Empty;
        private string m_RepeatedPassword = string.Empty;
        private string m_TooltipMessage;
        private bool m_IsConnected;
    }
}