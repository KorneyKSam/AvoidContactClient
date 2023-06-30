using System;
using System.ComponentModel;
using UnityEngine.Events;

namespace MVVM
{
    public class PropertyBinder<T> : IDisposable
    {
        private UnityEvent<T> m_ViewEvenet;
        private UnityAction<T> m_ViewModelAction;
        private bool m_SkipViewBinding;

        private UnityAction<T> m_ViewAction;
        private INotifyPropertyChanged m_PropertyOwner;
        private string m_OwnerPropertyName;
        private bool m_SkipViewModelBinding;

        public PropertyBinder(INotifyPropertyChanged notifyPropertyChanged, string ownerPropertyName, UnityAction<T> viewAction)
        {
            m_PropertyOwner = notifyPropertyChanged;
            m_OwnerPropertyName = ownerPropertyName;
            m_ViewAction = viewAction;
            m_PropertyOwner.PropertyChanged += PropertyChangedHandler;
        }

        public void CreateReverseBinding(UnityEvent<T> viewEvent, UnityAction<T> viewModelAction)
        {
            m_SkipViewModelBinding = false;
            m_ViewEvenet = viewEvent;
            m_ViewModelAction = viewModelAction;
            m_ViewEvenet.AddListener(OnViewEvent);
        }

        public void Dispose()
        {
            if (m_ViewEvenet != null)
            {
                m_ViewEvenet.RemoveListener(OnViewEvent);
            }
            m_PropertyOwner.PropertyChanged -= PropertyChangedHandler;
            m_PropertyOwner = null;
        }

        private void OnViewEvent(T arument)
        {
            if (m_SkipViewModelBinding)
            {
                m_SkipViewModelBinding = false;
                return;
            }

            m_SkipViewBinding = true;
            m_ViewModelAction?.Invoke(arument);
        }

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (m_SkipViewBinding)
            {
                m_SkipViewBinding = false;
                return;
            }

            if (e.PropertyName == m_OwnerPropertyName)
            {
                m_SkipViewModelBinding = true;
                m_ViewAction?.Invoke((T)sender.GetType().GetProperty(m_OwnerPropertyName).GetValue(sender, null));
            }
        }
    }
}
