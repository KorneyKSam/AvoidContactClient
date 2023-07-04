using MVVM;
using Networking;
using UnityWeld.Binding;
using Zenject;

namespace UI
{
    [Binding]
    public class SignInViewModel : BasePropertyChanged
    {
        [Inject]
        private SignInModel m_SignInModel;

        public SignInModel Model => m_SignInModel;

        private string m_Message;
        private bool m_IsAutomaticlAuthorization;

        [Binding]
        public string Login
        {
            get => m_SignInModel.Login;
            set => Set((value) => m_SignInModel.Login = value, m_SignInModel.Login, value);
        }

        [Binding]
        public string Password
        {
            get => m_SignInModel.Password;
            set => Set((value) => m_SignInModel.Password = value, m_SignInModel.Password, value);
        }

        [Binding]
        public string Message
        {
            get => m_Message;
            set => Set(ref m_Message, value);
        }

        [Binding]
        public bool IsAutomaticAuthorization
        {
            get => m_IsAutomaticlAuthorization;
            set => Set(ref m_IsAutomaticlAuthorization, value);
        }
    }
}