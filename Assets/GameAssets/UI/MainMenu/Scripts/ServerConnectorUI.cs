using Networking;
using System.Collections.Generic;
using UI.ViewModels;
using Zenject;

namespace MyNamespace
{
    public class ServerConnectorUI
    {
        private ServerConnector m_ServerConnector;

        private List<INetworkConnectionViewModel> m_NetworkListeners;

        [Inject]
        public ServerConnectorUI(ServerConnector serverConnector)
        {
            m_ServerConnector = serverConnector;
            m_NetworkListeners = new List<INetworkConnectionViewModel>();
            AddListeners();
        }

        ~ServerConnectorUI()
        {
            RemoveLisnteners();
        }

        public void AddConnectionListener(INetworkConnectionViewModel networkConnectionViewModel)
        {
            m_NetworkListeners.Add(networkConnectionViewModel);
            networkConnectionViewModel.IsConnected = m_ServerConnector.IsConnected;
            UpdateReconnectionStatus();
        }

        public void RemoveConnectionListener(INetworkConnectionViewModel networkConnectionViewModel)
        {
            m_NetworkListeners.Remove(networkConnectionViewModel);
            UpdateReconnectionStatus();
        }

        private void OnConnectionChanged(bool isConnected)
        {
            m_NetworkListeners.ForEach(m => m.IsConnected = isConnected);
        }

        private void AddListeners()
        {
            m_ServerConnector.OnConnectionChanged += OnConnectionChanged;
        }

        private void RemoveLisnteners()
        {
            m_ServerConnector.OnConnectionChanged -= OnConnectionChanged;
        }

        private void UpdateReconnectionStatus()
        {
            m_ServerConnector.IsLoopedReconnection = m_NetworkListeners.Count > 0;

            if (m_ServerConnector.IsLoopedReconnection && !m_ServerConnector.IsConnected)
            {
                m_ServerConnector.Connect();
            }
        }
    }
}