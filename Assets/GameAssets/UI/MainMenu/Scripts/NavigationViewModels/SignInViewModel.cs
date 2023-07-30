using MVVM;
using Networking;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using Zenject;

namespace UI
{
    [Binding]
    public class SignInViewModel : BaseMonoPropertyChanged
    {
        [SerializeField]
        private Button m_AuthorizationBtn;

        [SerializeField]
        private Button m_OfflineBtn;

        [SerializeField]
        private Button m_TrasitRegistrationBtn;

        [SerializeField]
        private Button m_ResetPasswordBtn;

        [Inject]
        private ISignerInfo m_SignerInfo;

        [Inject]
        private IServerConnectionInfo m_ConnectionInfo;

        private string m_Login;
        private string m_Password;
        private string m_Message;
        private bool m_IsConnected;
        private bool m_IsLoggedIn;

        public Button AuthorizationBtn => m_AuthorizationBtn;
        public Button OfflineBtn => m_OfflineBtn;
        public Button TransitToRegistrationBtn => m_TrasitRegistrationBtn;
        public Button ResetPasswordBtn => m_ResetPasswordBtn;

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
        public string Message
        {
            get => m_Message;
            set => Set(ref m_Message, value);
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
            Message = m_SignerInfo.FailedMessage;
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
                Message = m_SignerInfo.FailedMessage;
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