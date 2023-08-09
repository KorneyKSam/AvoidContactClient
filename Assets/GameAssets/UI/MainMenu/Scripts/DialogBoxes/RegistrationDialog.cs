using Common;
using DialogBoxService;
using Networking;
using UI.ViewModels;
using UnityEngine;
using UnityWeld.Binding;
using Zenject;

namespace UI.DialogBoxes
{
    [Binding]
    public class RegistrationDialog : ZoomedDialog
    {
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
                                                      SignMessages.NoConnection;
        }

        private void AddListeners()
        {
            m_RegistrationViewModel.RegistrationBtn.onClick.AddListener(OnRegistrationClick);
            m_RegistrationViewModel.CancelRegistration.onClick.AddListener(OnCancelClick);
            m_ServerConnector.OnConnectionChanged += UpdateConnectStatusView;
        }

        private void RemoveListeners()
        {
            m_RegistrationViewModel.RegistrationBtn.onClick.RemoveListener(OnRegistrationClick);
            m_RegistrationViewModel.CancelRegistration.onClick.RemoveListener(OnCancelClick);
            m_ServerConnector.OnConnectionChanged -= UpdateConnectStatusView;
        }

        private void OnRegistrationClick()
        {
            m_SignService.TryToSignUp(m_RegistrationViewModel.GetSignUpModel(), OnRegistrationResult);
        }

        private void OnRegistrationResult(bool success, string message)
        {

        }

        private void OnCancelClick()
        {

        }
    }
}