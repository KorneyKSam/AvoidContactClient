using Riptide;
using System;
using System.Collections.Generic;

namespace Networking
{
    public interface IClientConnectInfo
    {
        public event EventHandler<ClientConnectedEventArgs> OnClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> OnClientDisconnected;
        public List<int> ConnectedPlayerIDs { get; }
    }
}