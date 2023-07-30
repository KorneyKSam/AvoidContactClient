using MVVM;

namespace Networking
{
    public class ServerConnectionInfo : NotifyPropertyChanged, IServerConnectionInfo
    {
        private bool m_IsConnected;

        public bool IsConnected
        {
            get => m_IsConnected;
            set => Set(ref m_IsConnected, value);
        }
    }
}