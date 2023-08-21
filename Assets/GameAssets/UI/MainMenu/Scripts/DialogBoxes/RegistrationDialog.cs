using Common;
using DialogBoxService;
using Networking;
using Networking.Sign;
using UI.ViewModels;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;
using Zenject;

namespace UI.DialogBoxes
{
    [Binding]
    public class RegistrationDialog : ZoomedDialog
    {
        public event UnityAction OnCancelClick
        {
            add { m_RegistrationViewModel.CancelRegistration.onClick.AddListener(value); }
            remove { m_RegistrationViewModel.CancelRegistration.onClick.RemoveListener(value); }
        }

        [Inject]
        private DataService m_DataService;

        [Inject]
        private ServerConnector m_ServerConnector;

        [Inject]
        private SignService m_SignService;

        [SerializeField]
        private RegistrationViewModel m_RegistrationViewModel;

        private string m_LastTooltipMessage;

        public void Activate(bool isActive)
        {
            UpdateView();

            RemoveListeners();
            m_ServerConnector.IsLoopedReconnection = isActive;
            if (isActive)
            {
                AddListeners();
                if (!m_ServerConnector.IsConnected)
                {
                    m_ServerConnector.Connect();
                }
            }
        }

        private void UpdateView()
        {
            UpdateConnectStatusView(m_ServerConnector.IsConnected);
        }

        private void UpdateConnectStatusView(bool isConnected)
        {
            m_RegistrationViewModel.IsConnected = isConnected;
            m_RegistrationViewModel.TooltipMessage = isConnected ? m_LastTooltipMessage :
                                                      SignValidationMessages.NoConnection;
        }

        private void AddListeners()
        {
            m_RegistrationViewModel.RegistrationBtn.onClick.AddListener(OnRegistrationClick);
            m_ServerConnector.OnConnectionChanged += UpdateConnectStatusView;
        }

        private void RemoveListeners()
        {
            m_RegistrationViewModel.RegistrationBtn.onClick.RemoveListener(OnRegistrationClick);
            m_ServerConnector.OnConnectionChanged -= UpdateConnectStatusView;
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
                Login = m_RegistrationViewModel.Login,
                Email = m_RegistrationViewModel.Email,
                Password = m_RegistrationViewModel.Password
            };
        }
    }
}