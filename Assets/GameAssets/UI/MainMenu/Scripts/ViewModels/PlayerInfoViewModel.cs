using MVVM;
using MyNamespace;
using Networking.Sign;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using Zenject;

namespace UI.ViewModels
{
    [Binding]
    public class PlayerInfoViewModel : BaseMonoPropertyChanged, INetworkConnectionViewModel
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

        [Inject]
        private ServerConnectorUI m_ServerConnectorUI;

        [Inject]
        private SignService m_SignService;

        private void AddListeners()
        {
            RegistrationBtn.onClick.AddListener(OnRegistrationClick);
            m_ServerConnectorUI.AddConnectionListener(this);
        }

        private void RemoveListeners()
        {
            RegistrationBtn.onClick.RemoveListener(OnRegistrationClick);
            m_ServerConnectorUI.RemoveConnectionListener(this);
        }

        private void OnRegistrationClick()
        {
            m_SignService.TryToSignUp(GetSignUpInfo(), OnRegistrationResult);
        }

        private void OnRegistrationResult(SignUpResult signUpResult)
        {

        }

        private SignUpInfo GetSignUpInfo()
        {
            return new SignUpInfo()
            {
                Login = Login,
                Email = Email,
                Password = Password
            };
        }

        private string m_Login = string.Empty;
        private string m_Email = string.Empty;
        private string m_Password = string.Empty;
        private string m_RepeatedPassword = string.Empty;
        private string m_TooltipMessage;
        private bool m_IsConnected;
    }
}