using Riptide;
using Riptide.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Networking
{
    public class ServerConnector : MonoBehaviour, IInitializable, IConnectorInfo
    {
        public event Action<bool> OnConnectionChanged;

        public event EventHandler<ClientConnectedEventArgs> OnClientConnected
        {
            add { m_Client.ClientConnected += value; }
            remove { m_Client.ClientConnected -= value; }
        }

        public event EventHandler<ClientDisconnectedEventArgs> OnClientDisconnected
        {
            add { m_Client.ClientDisconnected += value; }
            remove { m_Client.ClientDisconnected -= value; }
        }
        public bool IsLoopedReconnection { get; set; }

        public bool IsConnected => m_Client.IsConnected;
        public List<int> ConnectedPlayerIDs => m_ConnectedPlayerIDs;

        [Header("Connection")]
        [SerializeField]
        private string m_Ip = "127.0.0.1";

        [SerializeField]
        private ushort m_Port = 7777;

        [Inject]
        private Client m_Client;

        private List<int> m_ConnectedPlayerIDs = new();
        private Action<bool> m_OnConnectionResultCallback;
        private bool m_IsConnecting;

        public void Initialize()
        {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
            AddHandlers();
        }

        public void Connect(Action<bool> resultCallback = null)
        {
            if (!m_IsConnecting)
            {
                m_IsConnecting = true;
                m_Client.Connect($"{m_Ip}:{m_Port}");
                m_OnConnectionResultCallback = resultCallback;
            }
        }

        private void AddHandlers()
        {
            m_Client.Connected += ConnectionHandler;
            m_Client.Disconnected += DisconnectedHandler;
            m_Client.ClientConnected += ClientConnetedHandler;
            m_Client.ConnectionFailed += ConnectionFailedHandler;
            m_Client.ClientDisconnected += ClientDisconnectedHandler;
        }

        private void RemoveHandlers()
        {
            m_Client.Connected -= ConnectionHandler;
            m_Client.Disconnected -= DisconnectedHandler;
            m_Client.ClientConnected -= ClientConnetedHandler;
            m_Client.ConnectionFailed -= ConnectionFailedHandler;
            m_Client.ClientDisconnected -= ClientDisconnectedHandler;
        }

        private void ConnectionHandler(object sender, EventArgs e)
        {
            m_IsConnecting = false;
            InvokeCallback(true);
            OnConnectionChanged?.Invoke(true);
        }

        private void DisconnectedHandler(object sender, DisconnectedEventArgs e)
        {
            InvokeCallback(false);
            OnConnectionChanged?.Invoke(false);
            TryToReconnect();
        }

        private void ConnectionFailedHandler(object sender, ConnectionFailedEventArgs e)
        {
            m_IsConnecting = false;
            InvokeCallback(false);
            TryToReconnect();
        }

        private void ClientConnetedHandler(object sender, ClientConnectedEventArgs e)
        {
            m_ConnectedPlayerIDs.Add(e.Id);
        }

        private void ClientDisconnectedHandler(object sender, ClientDisconnectedEventArgs e)
        {
            m_ConnectedPlayerIDs.Remove(e.Id);
        }

        private void TryToReconnect()
        {
            if (IsLoopedReconnection)
            {
                Connect();
            }
        }

        private void FixedUpdate()
        {
            m_Client.Update();
        }

        private void OnDestroy()
        {
            m_Client.Disconnect();
            RemoveHandlers();
        }

        private void OnApplicationQuit()
        {
            m_Client.Disconnect();
        }

        private void InvokeCallback(bool result)
        {
            m_OnConnectionResultCallback?.Invoke(result);
            m_OnConnectionResultCallback = null;
        }
    }
}