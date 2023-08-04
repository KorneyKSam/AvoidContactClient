using Common;
using Common.Data;
using Networking.Enums;
using Riptide;
using System;
using Zenject;

namespace Networking
{
    public class ServerSigner : IInitializable
    {
        public bool IsLogedIn => m_SignerInfo.IsLoggedIn;

        [Inject]
        private SignerInfo m_SignerInfo;

        [Inject]
        private MessageSender m_MessageSender;

        [Inject]
        private DataService m_DataService;

        [Inject]
        private ServerConnector m_ServerConnector;

        private bool m_IsSignerBusy;
        private Action<bool, string> m_ResultCallback;
        private string m_Login;
        private string m_Password;

        public void Initialize()
        {
            var signInData = m_DataService.Load<SignInData>();
            UpdateModelByData(signInData);
            SignEventsHolder.OnSignIn.AddListener(OnSignInResult);
            SignEventsHolder.OnSignUp.AddListener(OnSignUpResult);
            m_ServerConnector.OnServerDisconnected += OnServerDisconnected;
        }

        public void TryToSignIn(Action<bool, string> onResultCallback = null)
        {
            if (!m_IsSignerBusy)
            {
                if (m_SignerInfo.IsAutomaticAuthorization)
                {
                    TryToSignIn(m_SignerInfo.Login, m_SignerInfo.Password, onResultCallback);
                }
                else
                {
                    onResultCallback?.Invoke(false, string.Empty);
                }
            }
        }

        public void TryToSignIn(string login, string password, Action<bool, string> onResultCallback = null)
        {
            if (!m_IsSignerBusy)
            {
                m_IsSignerBusy = true;

                m_Login = login;
                m_Password = password;
                m_ResultCallback = onResultCallback;

                m_MessageSender.SignIn(m_Login, m_Password);
            }
        }

        public void TryToSignUp(SignUpModel signUpModel, Action<bool, string> onResultCallback = null)
        {
            //TODO : Client validation
            if (!m_IsSignerBusy)
            {
                m_ResultCallback = onResultCallback;
                m_MessageSender.SignUp(signUpModel);
            }
        }

        private void OnSignInResult(SignInResult signInResult)
        {
            bool success = signInResult == SignInResult.Success;
            m_ResultCallback?.Invoke(success, signInResult.ToString());
            if (success)
            {
                if (m_SignerInfo.IsAutomaticAuthorization)
                {
                    var newData = CreateDataFromModel();
                    m_DataService.Save(newData);
                }
            }
            m_IsSignerBusy = false;
        }


        private void OnSignUpResult(SignUpResult signUpResult)
        {
            m_ResultCallback?.Invoke(signUpResult == SignUpResult.Success, signUpResult.ToString());
        }

        private SignInData CreateDataFromModel()
        {
            return new SignInData()
            {
                IsAutomaticAuthorization = m_SignerInfo.IsAutomaticAuthorization,
                Login = m_SignerInfo.IsAutomaticAuthorization ? m_Login : string.Empty,
                Password = m_SignerInfo.IsAutomaticAuthorization ? m_Password : string.Empty,
            };         
        }

        private void UpdateModelByData(SignInData signInData)
        {
            m_SignerInfo.Login = signInData.Login;
            m_SignerInfo.Password = signInData.Password;
            m_SignerInfo.IsAutomaticAuthorization = signInData.IsAutomaticAuthorization;
        }

        private void OnServerDisconnected(object sender, DisconnectedEventArgs e)
        {
            m_ResultCallback?.Invoke(false, CommonNetworkMessages.Disconnected);
            m_IsSignerBusy = false;
        }
    }
}