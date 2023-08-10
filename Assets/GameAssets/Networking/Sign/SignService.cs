using System;
using UnityEngine;
using Zenject;

namespace Networking.Sign
{
    public class SignService : IInitializable
    {
        public bool IsLogedIn => !string.IsNullOrEmpty(m_AuthorizationToken);

        [Inject]
        private SignMessageSender m_MessageSender;

        [Inject]
        private IConnectorInfo m_ConnectorInfo;

        private Action<SignInResult> m_AuhtorizaitonCallback;
        private Action<SignUpResult> m_RegistrationCallback;
        private string m_AuthorizationToken;

        ~SignService()
        {
            RemoveListeners();
        }

        public void Initialize()
        {
            AddListeners();           
        }

        public void TryToSignIn(string login, string password, Action<SignInResult> onResultCallback = null)
        {
            if (m_ConnectorInfo.IsConnected && !IsLogedIn)
            {
                m_AuhtorizaitonCallback = onResultCallback;
                m_MessageSender.SignIn(login, password);
            }
        }

        public void TryToSignUp(SignUpModel signUpModel, Action<SignUpResult> onResultCallback = null)
        {

        }

        public void SignOut()
        {
            if (IsLogedIn)
            {
                m_AuthorizationToken = string.Empty;
                if (m_ConnectorInfo.IsConnected)
                {
                    m_MessageSender.SignOut();
                }
            }
        }

        public void LinkToken(string token)
        {
            if (m_ConnectorInfo.IsConnected)
            {
                m_MessageSender.LinkToken(token);
            }
        }

        private void AddListeners()
        {
            SignMessageReceiver.OnSignInResultRecieve.AddListener(OnSignInReceiveResult);
            SignMessageReceiver.OnSignUpResultReceive.AddListener(OnSignUpReceiveResult);
            m_ConnectorInfo.OnConnectionChanged += OnConnectionChanged;
        }

        private void RemoveListeners()
        {
            SignMessageReceiver.OnSignInResultRecieve.RemoveListener(OnSignInReceiveResult);
            SignMessageReceiver.OnSignUpResultReceive.RemoveListener(OnSignUpReceiveResult);
            m_ConnectorInfo.OnConnectionChanged -= OnConnectionChanged;
        }

        private void OnSignInReceiveResult(SignInResult result, string authorizationToken)
        {
            if (result == SignInResult.Success)
            {
                m_AuthorizationToken = authorizationToken;
                LinkToken(m_AuthorizationToken);
            }

            m_AuhtorizaitonCallback?.Invoke(result);
            m_AuhtorizaitonCallback = null;
            Debug.Log(result.ToString());
        }

        private void OnSignUpReceiveResult(SignUpResult result)
        {
            m_RegistrationCallback?.Invoke(result);
            m_RegistrationCallback = null;
        }

        private void OnConnectionChanged(bool isConnected)
        {
            if (isConnected && IsLogedIn)
            {
                LinkToken(m_AuthorizationToken);
            }
        }
    }
}