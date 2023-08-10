using System;

namespace Networking
{
    public interface IConnectorInfo
    {
        public event Action<bool> OnConnectionChanged;
        public bool IsConnected { get; }
    }
}