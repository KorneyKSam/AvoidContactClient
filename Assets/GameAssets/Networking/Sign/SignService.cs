using AdvancedDebugger;
using AvoidContactCommon.Sign;
using AvoidContactCommon.Validation;
using System;
using Zenject;

namespace Networking.Sign
{
    public class SignService
    {
        public bool IsLogedIn => !string.IsNullOrEmpty(m_AuthorizationToken);

        private SignMessageSender m_MessageSender;
        private CommonSignValidator m_CommonValidator;
        private IConnectorInfo m_ConnectorInfo;
        private Action<SignInResult> m_AuhtorizaitonCallback;
        private Action<SignUpResult> m_RegistrationCallback;
        private string m_AuthorizationToken;

        [Inject]
        public SignService(IConnectorInfo connectorInfo, SignMessageSender signMessageSender)
        {
            m_ConnectorInfo = connectorInfo;
            m_MessageSender = signMessageSender;
            m_CommonValidator = new CommonSignValidator();
            AddListeners();
        }

        ~SignService()
        {
            RemoveListeners();
        }

        public void TryToSignIn(string login, string password, Action<SignInResult> onResultCallback = null)
        {
            if (m_ConnectorInfo.IsConnected && !IsLogedIn)
            {
                var result = m_CommonValidator.CheckSignIn(login, password);
                if (result == SignInResult.Success)
                {
                    m_AuhtorizaitonCallback = onResultCallback;
                    m_MessageSender.SignIn(login, password);
                }
                else
                {
                    onResultCallback?.Invoke(result);
                }
            }
        }

        public void TryToSignUp(SignedPlayerInfo signedPlayerInfo, Action<SignUpResult> onResultCallback = null)
        {
            if (m_ConnectorInfo.IsConnected && !IsLogedIn)
            {
                var result = m_CommonValidator.CheckSignUp(signedPlayerInfo);
                if (result == SignUpResult.Success)
                {
                    m_RegistrationCallback = onResultCallback;
                    m_MessageSender.SignUp(signedPlayerInfo);
                }
                else
                {
                    onResultCallback?.Invoke(result);
                }
            }
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
            Debugger.Log(result.ToString(), DebuggerLog.InfoDebug);
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