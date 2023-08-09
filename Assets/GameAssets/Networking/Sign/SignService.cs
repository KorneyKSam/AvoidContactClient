using System;
using Zenject;

namespace Networking
{
    public class SignService : IInitializable
    {
        public bool IsLogedIn => m_IsLoggedIn;

        [Inject]
        private MessageSender m_MessageSender;

        private Action<bool> m_AuhtorizaitonCallback;
        private Action<bool, string> m_RegistrationCallback;
        private bool m_IsLoggedIn;

        ~SignService()
        {
            RemoveListeners();
        }

        public void Initialize()
        {
            AddListeners();
        }

        public void TryToSignIn(string login, string password, Action<bool> onResultCallback = null)
        {
            m_AuhtorizaitonCallback = onResultCallback;
            m_MessageSender.SignIn(login, password);
        }

        public void TryToSignUp(SignUpModel signUpModel, Action<bool, string> onResultCallback = null)
        {

        }

        private void AddListeners()
        {
            SignMessageReceiver.OnSignInResultRecieve.AddListener(OnSignInReceiveResult);
            SignMessageReceiver.OnSignUpResultReceive.AddListener(OnSignUpReceiveResult);
        }

        private void RemoveListeners()
        {
            SignMessageReceiver.OnSignInResultRecieve.RemoveListener(OnSignInReceiveResult);
            SignMessageReceiver.OnSignUpResultReceive.RemoveListener(OnSignUpReceiveResult);
        }

        private void OnSignInReceiveResult(bool sucess)
        {
            m_AuhtorizaitonCallback?.Invoke(sucess);
            m_AuhtorizaitonCallback = null;
        }
        private void OnSignUpReceiveResult(bool sucess, string message)
        {
            m_RegistrationCallback?.Invoke(sucess, message);
            m_RegistrationCallback = null;
        }
    }
}