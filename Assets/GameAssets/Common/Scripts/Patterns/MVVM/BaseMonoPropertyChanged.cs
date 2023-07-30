using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MVVM
{
    public abstract class BaseMonoPropertyChanged : MonoBehaviour, INotifyPropertyChanged
    {
        private NotifyPropertyChanged m_NotifyPropertyChanged;
        private NotifyPropertyChanged NotifyPropertyChanged => m_NotifyPropertyChanged ??= new NotifyPropertyChanged(this);

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { NotifyPropertyChanged.PropertyChanged += value; }
            remove { NotifyPropertyChanged.PropertyChanged -= value; }
        }

        protected virtual bool Set<T>(ref T member, T value, [CallerMemberName] string propertyName = null)
        {
            return NotifyPropertyChanged.Set(ref member, value, propertyName);
        }

        protected virtual bool Set<T>(Action<T> setter, T field, T value, [CallerMemberName] string propertyName = "")
        {
            return NotifyPropertyChanged.Set(setter, field, value, propertyName);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => NotifyPropertyChanged.OnPropertyChanged(propertyName);
    }
}