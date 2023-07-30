using Riptide;
using Riptide.Utils;
using SceneLoading;
using System;
using UnityEngine;
using Zenject;

namespace Networking
{
    public class ServerConnector : MonoBehaviour, IInitializable
    {
        public bool IsConnected => m_ServerConnectionInfo.IsConnected;

        [Header("Connection")]
        [SerializeField]
        private string m_Ip = "127.0.0.1";

        [SerializeField]
        private ushort m_Port = 7777;

        [Header("Fake loading")]
        [SerializeField]
        private int m_LoadingMilliseconds = 1000;

        [Inject]
        private Client m_Client;

        [Inject]
        private FakeLoader m_FakeLoader;

        [Inject]
        private ServerConnectionInfo m_ServerConnectionInfo;

        private Action<bool> m_OnConnectionResult;

        public void Initialize()
        {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
            AddHandlersForClient(m_Client);
        }

        public void Connect(bool showLoadingScreen = false, Action<bool> onConnectionResult = null)
        {
            m_OnConnectionResult = onConnectionResult;

            if (showLoadingScreen)
            {
                m_FakeLoader.ShowFakeLoadAnimation(m_LoadingMilliseconds, allowActivation: false, useResumeButton: false);
            }

            m_Client.Connect($"{m_Ip}:{m_Port}");
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
            m_ServerConnectionInfo.IsConnected = true;
            DisableLoader();
            m_OnConnectionResult?.Invoke(true);
        }

        private void ConnectionFailedHandler(object sender, ConnectionFailedEventArgs e)
        {
            m_ServerConnectionInfo.IsConnected = false;
            DisableLoader();
            m_OnConnectionResult?.Invoke(false);
        }

        private void DisconnectedHandler(object sender, DisconnectedEventArgs e)
        {
            m_ServerConnectionInfo.IsConnected = false;
        }

        private void ClientDisconnectedHandler(object sender, ClientDisconnectedEventArgs e)
        {

        }

        private void DisableLoader()
        {
            if (m_FakeLoader.IsLoading)
            {
                m_FakeLoader.AllowActivation(true);
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