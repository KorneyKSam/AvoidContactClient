using Common;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public NavigationPanel NavigationPanel => m_NavigationPanel;

        [SerializeField]
        private NavigationPanel m_NavigationPanel;

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