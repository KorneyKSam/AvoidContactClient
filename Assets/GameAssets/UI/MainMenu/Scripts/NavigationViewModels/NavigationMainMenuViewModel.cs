using MVVM;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace UI
{
    [Binding]
    public class NavigationMainMenuViewModel : BaseMonoPropertyChanged
    {
        [SerializeField]
        private Button m_MultiplayerBtn;

        [SerializeField]
        private Button m_SignleplayerBtn;

        [SerializeField]
        private Button m_DeveloperBtn;

        [SerializeField]
        private Button m_AchievementsBtn;

        [SerializeField]
        private Button m_SettingsBtn;

        [SerializeField]
        private Button m_ExitBtn;

        public Button MultiplayerBtn => m_MultiplayerBtn;
        public Button SignleplayerBtn => m_SignleplayerBtn;
        public Button DeveloperBtn => m_DeveloperBtn;
        public Button AchievementsBtn => m_AchievementsBtn;
        public Button SettingsBtn => m_SettingsBtn;
        public Button ExitBtn => m_ExitBtn;
    }
}