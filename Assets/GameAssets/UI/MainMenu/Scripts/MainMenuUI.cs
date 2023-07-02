using Common;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public NavigationPanelView NavigationPanel => m_NavigationPanel;

        [SerializeField]
        private NavigationPanelView m_NavigationPanel;

        [Inject]
        private CameraController m_CameraController;

        public void Initialize()
        {
            m_NavigationPanel.Init(m_CameraController);
        }

        private void Start()
        {
            Initialize();
        }
    }
}