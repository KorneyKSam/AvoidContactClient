using Riptide;
using System;
using System.Collections.Generic;

namespace Networking
{
    public interface IConnectorInfo
    {
        public event Action<bool> OnConnectionChanged;
        public event EventHandler<ClientConnectedEventArgs> OnClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> OnClientDisconnected;

        public bool IsConnected { get; }
        public List<int> ConnectedPlayerIDs { get; }
    }
}