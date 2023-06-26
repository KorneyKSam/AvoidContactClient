using UnityEngine;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public NavigationPanel NavigationPanel => m_NavigationPanel;

        [SerializeField]
        private NavigationPanel m_NavigationPanel;

        [SerializeField]
        private CameraController m_CameraController;

        public void Init()
        {
            m_NavigationPanel.Init(m_CameraController);
        }
    }
}