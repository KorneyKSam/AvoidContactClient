using Common;
using Common.Data;
using Networking;
using SceneLoading;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Network Connection")]
        [SerializeField]
        private string m_Ip = "127.0.0.1";

        [SerializeField]
        private ushort m_Port = 7777;

        [Header("Fake loading")]
        [SerializeField]
        private int m_LoadingMilliseconds = 1000;

        [Inject]
        private NavigationPanelConroller m_NavigationPanel;

        [Inject]
        private FakeLoader m_FakeLoader;

        [Inject]
        private NetworkService m_NetworkService;

        [Inject]
        private DataService m_DataService;

        [Inject]
        private SignController m_SignController;

        [Inject]
        private CameraController m_CameraController;

        private GlobalData m_GlobalData;

        public void Initialize()
        {

            m_GlobalData = m_DataService.Load<GlobalData>();
            m_NavigationPanel.Init(m_CameraController);
            ShowMainContent();

            if (!m_NetworkService.IsConnected)
            {
                StartConnecting();
            }
        }

        private void Awake()
        {
            Initialize();
        }

        private void StartConnecting()
        {
            m_FakeLoader.ShowFakeLoadAnimation(m_LoadingMilliseconds, allowActivation: false, useResumeButton: false);
            m_NetworkService.Connect(m_Ip, m_Port);
            m_NetworkService.OnConnectionResult += OnConnectionResult;
        }

        private void OnConnectionResult(bool isConnected)
        {
            m_FakeLoader.AllowActivation(true);

            if (isConnected)
            {
                if (m_GlobalData.IsAutomaticAuthorization)
                {
                    OnAuthorization(m_NavigationPanel.SignInModel);
                }
            }
        }

        private void ShowAuthorizationPanel()
        {
            m_NavigationPanel.UpdateSignInView(m_GlobalData.IsAutomaticAuthorization);
            m_NavigationPanel.MoveToMiddle();
            m_NavigationPanel.SetNavigationContent(NavigationPanelContent.Authorization);
            AddAuthorizationListeners();
        }

        private void ShowRegistrationPanel()
        {
            RemoveAuthorizationListeners();
            m_NavigationPanel.MoveToMiddle();
            m_NavigationPanel.SetNavigationContent(NavigationPanelContent.Registraction);
        }

        private void ShowMainContent()
        {
            RemoveAuthorizationListeners();
            m_NavigationPanel.MoveToLeft();
            m_NavigationPanel.SetNavigationContent(NavigationPanelContent.MainContent);
        }

        private void OnAuthorization(SignInModel signInModel)
        {
            m_NetworkService.MessageSender.SignIn(signInModel);
        }

        private void OnSignIn(bool success, string message)
        {
            if (success)
            {
                ShowMainContent();
            }
            else
            {
                m_NavigationPanel.SetLogInFailed(message);
            }
        }


        private void AddAuthorizationListeners()
        {
            m_NavigationPanel.OnAuthorizationClick += OnAuthorization;
            SignEventsHolder.OnSignIn.AddListener(OnSignIn);
        }

        private void RemoveAuthorizationListeners()
        {
            m_NavigationPanel.OnAuthorizationClick -= OnAuthorization;
            SignEventsHolder.OnSignIn.RemoveListener(OnSignIn);
        }
    }
}