using MVVM;
using System.ComponentModel;

namespace Networking
{
    public interface ISignerInfo : INotifyPropertyChanged
    {
        public bool IsAutomaticAuthorization { get; set; }
        public string Login { get; }
        public string Password { get; }
        public string FailedMessage { get; }
        public bool IsLoggedIn{ get; }
    }
}