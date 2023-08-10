using Common;
using DialogBoxService;
using Networking;
using Networking.Sign;
using Networking.Sign.Data;
using System;
using System.Collections;
using UI.ViewModels;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;
using Zenject;

namespace UI.DialogBoxes
{
    [Binding]
    public class AuthorizationDialog : ZoomedDialog
    {
        public event UnityAction OnRegistrationClick
        {
            add { m_AuthorizationViewModel.TransitToRegistrationBtn.onClick.AddListener(value); }
            remove { m_AuthorizationViewModel.TransitToRegistrationBtn.onClick.RemoveListener(value); }
        }

        public event UnityAction OnCancelClick
        {
            add { m_AuthorizationViewModel.CancelButton.onClick.AddListener(value); }
            remove { m_AuthorizationViewModel.CancelButton.onClick.RemoveListener(value); }
        }

        private const float CancelingDelayAfterSuccessAuthorization = 1.5f;

        [Inject]
        private DataService m_DataService;

        [Inject]
        private ServerConnector m_ServerConnector;

        [Inject]
        private SignService m_SignService;

        [SerializeField]
        private AuthorizationViewModel m_AuthorizationViewModel;

        private AuthorizationData m_AuthorizationData;

        private string m_LastTooltipMessage;

        public override void Activate(bool isActive, float duration, Action onCompleteAnimation = null)
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

            base.Activate(isActive, duration, onCompleteAnimation);
        }

        private void UpdateView()
        {
            m_AuthorizationData = m_DataService.Load<AuthorizationData>();
            UpdateViewModelByData();
            UpdateConnectStatusView(m_ServerConnector.IsConnected);
        }

        private void UpdateViewModelByData()
        {
            m_AuthorizationViewModel.Login = m_AuthorizationData.Login;
            m_AuthorizationViewModel.Password = m_AuthorizationData.Password;
            m_AuthorizationViewModel.IsAutomaticAuthorization = m_AuthorizationData.IsAutomaticAuthorization;
        }

        private void UpdateDataByViewModel()
        {
            m_AuthorizationData.Login = m_AuthorizationViewModel.Login;
            m_AuthorizationData.Password = m_AuthorizationViewModel.Password;
            m_AuthorizationData.IsAutomaticAuthorization = m_AuthorizationViewModel.IsAutomaticAuthorization;
        }

        private void UpdateConnectStatusView(bool isConnected)
        {
            m_AuthorizationViewModel.IsConnected = isConnected;
            m_AuthorizationViewModel.TooltipMessage = isConnected ? m_LastTooltipMessage :
                                                      SignValidationMessages.NoConnection;
        }

        private void AddListeners()
        {
            m_AuthorizationViewModel.AuthorizationButton.onClick.AddListener(OnAuthorizationClick);
            m_AuthorizationViewModel.ResetPasswordBtn.onClick.AddListener(OnResetClick);
            m_ServerConnector.OnConnectionChanged += UpdateConnectStatusView;
        }

        private void RemoveListeners()
        {
            m_AuthorizationViewModel.AuthorizationButton.onClick.RemoveListener(OnAuthorizationClick);
            m_AuthorizationViewModel.ResetPasswordBtn.onClick.RemoveListener(OnResetClick);
            m_ServerConnector.OnConnectionChanged -= UpdateConnectStatusView;
        }

        private void OnAuthorizationClick()
        {
            m_SignService.TryToSignIn(m_AuthorizationViewModel.Login, m_AuthorizationViewModel.Password, OnAuthorizationResult);
        }

        private void OnAuthorizationResult(SignInResult signInResult)
        {                  
            m_LastTooltipMessage = SignValidationMessages.SignInMessages[signInResult];
            m_AuthorizationViewModel.TooltipMessage = m_LastTooltipMessage;

            if (signInResult == SignInResult.Success)
            {
                m_AuthorizationViewModel.IsAuthorized = true;
                StartCoroutine(InvokeCancel(CancelingDelayAfterSuccessAuthorization));
            }

            if (signInResult == SignInResult.Success || signInResult == SignInResult.AccountIsOccupied && m_AuthorizationViewModel.IsAutomaticAuthorization)
            {
                UpdateDataByViewModel();
            }
            else if (signInResult != SignInResult.Success || signInResult != SignInResult.AccountIsOccupied && m_AuthorizationViewModel.IsAutomaticAuthorization)
            {
                m_AuthorizationData.IsAutomaticAuthorization = false;
                m_AuthorizationViewModel.IsAutomaticAuthorization = false;
            }
            m_DataService.Save(m_AuthorizationData);
        }

        private IEnumerator InvokeCancel(float dealy)
        {
            yield return new WaitForSeconds(dealy);
            m_AuthorizationViewModel.CancelButton.onClick?.Invoke();
        }

        private void OnResetClick()
        {

        }
    }
}