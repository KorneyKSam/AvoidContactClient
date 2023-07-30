using System.ComponentModel;

namespace Networking
{
    public interface IServerConnectionInfo : INotifyPropertyChanged
    {
        public bool IsConnected { get; }
    }
}