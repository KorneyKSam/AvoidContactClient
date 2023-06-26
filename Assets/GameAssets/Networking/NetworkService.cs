using Riptide;
using Riptide.Utils;
using System;
using UnityEngine;
using Zenject;

namespace Networking
{
    public class NetworkService : MonoBehaviour
    {
        public bool IsConnected => m_Client.IsConnected;

        [Inject]
        private ISignServerCommandsExecutor m_ServerCommandsExecutor;

        private Client m_Client;
        private MessageSender m_MessageSender;
        private bool m_Initialized;

        public void Init()
        {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
            m_Client = new Client();
            AddHandlersForClient(m_Client);
            m_MessageSender = new MessageSender(m_Client);
            MessageReceiver.SetServerCommandsExecutor(m_ServerCommandsExecutor);
            m_Initialized = true;
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

        }

        private void ConnectionFailedHandler(object sender, ConnectionFailedEventArgs e)
        {

        }

        private void DisconnectedHandler(object sender, DisconnectedEventArgs e)
        {

        }

        private void ClientDisconnectedHandler(object sender, ClientDisconnectedEventArgs e)
        {

        }

        private void Awake()
        {
            if (!m_Initialized)
            {
                Init();
            }
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