using MVVM;
using MyNamespace;
using Networking.Sign;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using Zenject;

namespace MVVM.ViewModels
{
    [Binding]
    public class PlayerInfoViewModel : BaseMonoPropertyChanged, INetworkConnectionViewModel
    {
        public Button UpdateBtn => m_UpdateButton;
        public Button CancelUpdateBtn => m_CancelUpdateButton;

        [Binding]
        public string CallSign
        {
            get => m_CallSign;
            set => Set(ref m_CallSign, value);
        }

        [Binding]
        public string Description
        {
            get => m_Description;
            set => Set(ref m_Description, value);
        }

        [Binding]
        public string TooltipMessage
        {
            get => m_TooltipMessage;
            set => Set(ref m_TooltipMessage, value);
        }

        [Binding]
        public bool IsConnected
        {
            get => m_IsConnected;
            set => Set(ref m_IsConnected, value);
        }

        [SerializeField]
        private Button m_UpdateButton;

        [SerializeField]
        private Button m_CancelUpdateButton;

        [Inject]
        private ServerConnectorUI m_ServerConnectorUI;

        [Inject]
        private SignService m_SignService;

        private void AddListeners()
        {
            m_ServerConnectorUI.AddConnectionListener(this);
        }

        private void RemoveListeners()
        {
            m_ServerConnectorUI.RemoveConnectionListener(this);
        }

        private string m_CallSign = string.Empty;
        private string m_Description = string.Empty;
        private string m_Password = string.Empty;
        private string m_RepeatedPassword = string.Empty;
        private string m_TooltipMessage;
        private bool m_IsConnected;
    }
}