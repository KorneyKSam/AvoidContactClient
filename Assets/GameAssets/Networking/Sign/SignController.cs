using Common.Data;
using UI;
using Zenject;

namespace Networking
{
    public class SignController
    {
        public bool IsLoggedIn => m_IsLoggedIn;
        private bool m_IsLoggedIn;

        [Inject]
        private NetworkService m_NetworkService;

        [Inject]
        private MainMenuController m_MainMenuUI;

        [Inject]
        private DataService m_DataService;

        public void StartSign(bool isConnectedToServer)
        {

        }


    }
}