using MVVM;
using Networking;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using Zenject;

namespace DialogBoxService
{
    [Binding]
    public class AuthorizationDialog : BaseMonoPropertyChanged, IDialogBox
    {
        public Transform Transform => gameObject.transform;

        public Button AuthorizationBtn => m_AuthorizationBtn;
        public Button OfflineBtn => m_CancelBtn;
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
        public string TooltipMessage
        {
            get => m_TooltipMessage;
            set => Set(ref m_TooltipMessage, value);
        }

        [Binding]
        public bool IsLoggedIn
        {
            get => m_IsLoggedIn;
            private set => Set(ref m_IsLoggedIn, value);
        }

        [Binding]
        public bool IsAutomaticAuthorization
        {
            get => m_SignerInfo.IsAutomaticAuthorization;
            set => Set((value) => m_SignerInfo.IsAutomaticAuthorization = value, m_SignerInfo.IsAutomaticAuthorization, value);
        }

        [Binding]
        public bool IsConnected
        {
            get => m_IsConnected;
            set => Set(ref m_IsConnected, value);
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

        [Inject]
        private ISignerInfo m_SignerInfo;

        [Inject]
        private IServerConnectionInfo m_ConnectionInfo;

        private string m_Login;
        private string m_Password;
        private string m_TooltipMessage;
        private bool m_IsConnected;
        private bool m_IsLoggedIn;

        public void Init()
        {
            UpdateProperties();
            m_SignerInfo.PropertyChanged += ServerInfoPropertyChange;
            m_ConnectionInfo.PropertyChanged += ServerInfoPropertyChange;
        }

        private void UpdateProperties()
        {
            Login = m_SignerInfo.Login;
            Password = m_SignerInfo.Password;
            TooltipMessage = m_SignerInfo.FailedMessage;
            IsLoggedIn = m_SignerInfo.IsLoggedIn;
            IsConnected = m_ConnectionInfo.IsConnected;
            IsAutomaticAuthorization = m_SignerInfo.IsAutomaticAuthorization;
        }

        private void ServerInfoPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_SignerInfo.Login))
            {
                Login = m_SignerInfo.Login;
            }
            else if (e.PropertyName == nameof(m_SignerInfo.Password))
            {
                Password = m_SignerInfo.Password;
            }
            else if (e.PropertyName == nameof(m_SignerInfo.FailedMessage))
            {
                TooltipMessage = m_SignerInfo.FailedMessage;
            }
            else if (e.PropertyName == nameof(m_SignerInfo.IsLoggedIn))
            {
                IsLoggedIn = m_SignerInfo.IsLoggedIn;
            }
            else if (e.PropertyName == nameof(m_ConnectionInfo.IsConnected))
            {
                IsConnected = m_ConnectionInfo.IsConnected;
            }
            else if (e.PropertyName == nameof(m_SignerInfo.IsLoggedIn))
            {
                IsAutomaticAuthorization = m_SignerInfo.IsAutomaticAuthorization;
            }
        }
    }
}