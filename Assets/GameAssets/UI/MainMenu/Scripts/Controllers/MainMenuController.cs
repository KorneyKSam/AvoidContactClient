using Common;
using Networking;
using UI.Popups;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenuController : MonoBehaviour, IInitializable
    {
        [Inject]
        private ServerConnector m_ServerConnector;

        [Inject]
        private ServerSigner m_ServerSigner;

        [Inject]
        private NavigationPanelConroller m_NavigationPanel;

        [Inject]
        private CameraController m_CameraController;

        [Inject]
        private PopupController m_PopupController;

        private SignInViewModel SignInViewModel => m_NavigationPanel.SignInViewModel;
        private SignUpViewModel SignUpViewModel => m_NavigationPanel.SignUpViewModel;
        private NavigationMainMenuViewModel NavigationMainMenuViewModel => m_NavigationPanel.NavigationMainMenuViewModel;

        public void Initialize()
        {
            if (!m_ServerConnector.IsConnected)
            {
                StartConnecting(showLoadingScreen: true);
            }

            m_NavigationPanel.Init(m_CameraController);
            ShowMainContent();

            AddMainMenuListeners();
            AddAuthorizationListeners();
            AddRegistrationListeners();
        }

        private void StartConnecting(bool showLoadingScreen)
        {
            m_ServerConnector.Connect(showLoadingScreen, (isConnected) =>
            {
                if (isConnected)
                {
                    m_ServerSigner.TryToSignIn((isSignedIn, message) =>
                    {
                        if (isSignedIn)
                        {
                            RemoveSignListeners();
                        }
                    });
                }
            });
        }

        private void ShowMainContent()
        {
            m_NavigationPanel.MoveToLeft();
            m_NavigationPanel.SetNavigationContent(NavigationPanelContent.MainContent);
        }

        private void ShowAuthorizationPanel()
        {
            m_NavigationPanel.MoveToMiddle();
            m_NavigationPanel.SetNavigationContent(NavigationPanelContent.Authorization);

            if (!m_ServerConnector.IsConnected)
            {
                StartConnecting(showLoadingScreen: false);
            }
        }

        private void ShowRegistrationPanel()
        {
            m_NavigationPanel.MoveToMiddle();
            m_NavigationPanel.SetNavigationContent(NavigationPanelContent.Registraction);
        }

        private void OnMultiplayerClick()
        {
            if (!m_ServerSigner.IsLogedIn)
            {
                m_PopupController.ShowConfirmationPopup(ConfirmationPopupType.LogIn, (isConfirmed) =>
                {
                    if (isConfirmed)
                    {
                        ShowAuthorizationPanel();
                    }
                });
            }
        }

        private void OnAuthorizationClick()
        {
            m_ServerSigner.TryToSignIn(SignInViewModel.Login, SignInViewModel.Password, (success, message) =>
            {
                if (success)
                {
                    ShowMainContent();
                    RemoveSignListeners();
                }
                else
                {
                    m_NavigationPanel.SetLogInFailed(message);
                }
            });
        }

        private void Registration()
        {
            m_ServerSigner.TryToSignUp(SignUpViewModel.GetSignUpModel());
        }

        private void RemoveSignListeners()
        {
            RemoveAuthorizationListeners();
            RemoveRegistrationListeners();
        }

        private void AddMainMenuListeners()
        {
            RemoveMainMenuListeners();
            NavigationMainMenuViewModel.MultiplayerBtn.onClick.AddListener(OnMultiplayerClick);
        }

        private void RemoveMainMenuListeners()
        {
            NavigationMainMenuViewModel.MultiplayerBtn.onClick.RemoveListener(OnMultiplayerClick);
        }

        private void AddAuthorizationListeners()
        {
            RemoveAuthorizationListeners();
            SignInViewModel.OfflineBtn.onClick.AddListener(ShowMainContent);
            SignInViewModel.AuthorizationBtn.onClick.AddListener(OnAuthorizationClick);
            SignInViewModel.TransitToRegistrationBtn.onClick.AddListener(ShowRegistrationPanel);
        }

        private void RemoveAuthorizationListeners()
        {
            SignInViewModel.OfflineBtn.onClick.RemoveListener(ShowMainContent);
            SignInViewModel.AuthorizationBtn.onClick.RemoveListener(OnAuthorizationClick);
            SignInViewModel.TransitToRegistrationBtn.onClick.RemoveListener(ShowRegistrationPanel);
        }

        private void AddRegistrationListeners()
        {
            RemoveRegistrationListeners();
            SignUpViewModel.RegistrationBtn.onClick.AddListener(Registration);
            SignUpViewModel.CancelRegistration.onClick.AddListener(ShowMainContent);
        }

        private void RemoveRegistrationListeners()
        {
            SignUpViewModel.RegistrationBtn.onClick.RemoveListener(Registration);
            SignUpViewModel.CancelRegistration.onClick.RemoveListener(ShowMainContent);
        }
    }
}