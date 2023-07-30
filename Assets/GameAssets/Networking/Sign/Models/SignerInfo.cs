using MVVM;

namespace Networking
{
    public class SignerInfo : NotifyPropertyChanged, ISignerInfo
    {
        private string m_Login = string.Empty;
        private string m_Password = string.Empty;
        private string m_FailedMessage = string.Empty;
        private bool m_IsAutomaticAuthorization;
        private bool m_IsLoggedIn;

        public string Login 
        { 
            get => m_Login;
            set => Set(ref m_Login, value);
        }

        public string Password
        {
            get => m_Password;
            set => Set(ref m_Password, value);
        }

        public string FailedMessage
        {
            get => m_FailedMessage;
            set => Set(ref m_FailedMessage, value);
        }

        public bool IsAutomaticAuthorization
        {
            get => m_IsAutomaticAuthorization;
            set => Set(ref m_IsAutomaticAuthorization, value);
        }

        public bool IsLoggedIn
        {
            get => m_IsLoggedIn;
            set => Set(ref m_IsLoggedIn, value);
        }
    }
}