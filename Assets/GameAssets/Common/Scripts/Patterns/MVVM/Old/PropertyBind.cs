using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine.Events;

namespace MVVM
{
    public class PropertyBind<T> : IDisposable
    {
        public string PropertyName => m_OwnerPropertyName;

        private UnityEvent<T> m_ViewEvenet;
        private PropertyInfo m_PropertyInfo;
        private bool m_SkipViewBinding;

        private UnityAction<T> m_ViewAction;
        private INotifyPropertyChanged m_PropertyOwner;
        private string m_OwnerPropertyName;
        private bool m_SkipViewModelBinding;

        public PropertyBind(INotifyPropertyChanged notifyPropertyChanged, string ownerPropertyName)
        {
            m_PropertyOwner = notifyPropertyChanged;
            m_OwnerPropertyName = ownerPropertyName;
        }

        public void SetViewBinding(UnityAction<T> viewAction)
        {
            m_SkipViewBinding = false;
            m_ViewAction = viewAction;
            m_PropertyOwner.PropertyChanged += PropertyChangedHandler;
        }

        public void SetViewModelBinding(UnityEvent<T> viewEvent)
        {
            m_SkipViewModelBinding = false;
            m_ViewEvenet = viewEvent;
            m_PropertyInfo = m_PropertyOwner.GetType().GetProperty(m_OwnerPropertyName);
            m_ViewEvenet.AddListener(ViewEventHandler);
        }

        public void SetTwoWayBinding(UnityAction<T> viewAction, UnityEvent<T> viewEvent)
        {
            SetViewBinding(viewAction);
            SetViewModelBinding(viewEvent);
        }

        public void Dispose()
        {
            m_ViewEvenet?.RemoveListener(ViewEventHandler);
            m_PropertyOwner.PropertyChanged -= PropertyChangedHandler;
            m_PropertyOwner = null;
        }

        private void ViewEventHandler(T arument)
        {
            if (m_SkipViewModelBinding)
            {
                m_SkipViewModelBinding = false;
                return;
            }

            m_SkipViewBinding = true;
            m_PropertyInfo.SetValue(m_PropertyOwner, arument);
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