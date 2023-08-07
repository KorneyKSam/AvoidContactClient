using MVVM;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace UI
{
    [Binding]
    public class DeskUI : BaseMonoPropertyChanged
    {
        [SerializeField]
        private Button m_MultiplayerBtn;

        [SerializeField]
        private Button m_SignleplayerBtn;

        [SerializeField]
        private Button m_SettingsBtn;

        public Button MultiplayerBtn => m_MultiplayerBtn;
        public Button SignleplayerBtn => m_SignleplayerBtn;
        public Button SettingsBtn => m_SettingsBtn;
    }
}