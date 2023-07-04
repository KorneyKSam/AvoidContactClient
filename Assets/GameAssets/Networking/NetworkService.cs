using Riptide;
using Riptide.Utils;
using System;
using UnityEngine;
using Zenject;

namespace Networking
{
    public class NetworkService : MonoBehaviour, IInitializable
    {
        public event Action<bool> OnConnectionResult;
        public MessageSender MessageSender => m_MessageSender;

        public bool IsConnected => m_Client.IsConnected;

        private Client m_Client;
        private MessageSender m_MessageSender;

        public void Initialize()
        {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
            m_Client = new Client();
            AddHandlersForClient(m_Client);
            m_MessageSender = new MessageSender(m_Client);
        }

        public void Connect(string ip, ushort port)
        {
            m_Client.Connect($"{ip}:{port}");
        }

        private void AddHandlersForClient(Client client)
        {
            client.Connected += ConnectionHandler;
            client.ConnectionFailed += ConnectionFailedHandler;
            client.Disconnected += DisconnectedHandler;
            client.ClientDisconnected += ClientDisconnectedHandler;
        }

        private void ConnectionHandler(object sender, EventArgs e)
        {
            OnConnectionResult?.Invoke(true);
        }

        private void ConnectionFailedHandler(object sender, ConnectionFailedEventArgs e)
        {
            OnConnectionResult?.Invoke(false);
        }

        private void DisconnectedHandler(object sender, DisconnectedEventArgs e)
        {

        }

        private void ClientDisconnectedHandler(object sender, ClientDisconnectedEventArgs e)
        {

        }

        private void FixedUpdate()
        {
            m_Client.Update();
        }

        private void OnApplicationQuit()
        {
            m_Client.Disconnect();
        }
    }
}