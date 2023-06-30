using MVVM;
using UnityWeld.Binding;

namespace UI
{
    public class SignInViewModel : BaseViewModel
    {
        private string m_Login;
        private string m_Password;
        private bool m_IsAutomaticlAuthorization;

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
        public bool IsAutomaticAuthorization
        {
            get => m_IsAutomaticlAuthorization;
            set => Set(ref m_IsAutomaticlAuthorization, value);
        }
    }
}