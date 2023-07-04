using MVVM;
using UnityWeld.Binding;

namespace UI
{
    [Binding]
    public class SignUpViewModel : BasePropertyChanged
    {
        private string m_Login;
        private string m_Email;
        private string m_Password;
        private string m_RepeatedPassword;

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
    }
}