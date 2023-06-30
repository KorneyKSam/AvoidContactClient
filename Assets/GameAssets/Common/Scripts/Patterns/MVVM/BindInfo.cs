using System.ComponentModel;

namespace MVVM
{
    public class BindInfo
    {
        public INotifyPropertyChanged PropertyOwner { get; set; }
        public string OwnerPropertyName { get; set; }
        public object Property { get; set; }
    }
}
