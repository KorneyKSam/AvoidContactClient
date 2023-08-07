using DialogBoxService;
using Networking;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenuController : MonoBehaviour, IInitializable
    {
        [SerializeField]
        private AuthorizationDialog m_AuthorizationDialog;

        [SerializeField]
        private RegistrationDialog m_RegistrationDialog;

        [SerializeField]
        private MainScreen m_MainScreen;

        [Inject]
        private ServerConnector m_ServerConnector;

        [Inject]
        private ServerSigner m_ServerSigner;

        [Inject]
        private DialogService m_DialogService;

        public void Initialize()
        {
            if (!m_ServerConnector.IsConnected)
            {
                StartConnecting(showLoadingScreen: true);
            }

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

        private void ShowAuthorizationPanel()
        {
            m_DialogService.Open<AuthorizationDialog>();

            if (!m_ServerConnector.IsConnected)
            {
                StartConnecting(showLoadingScreen: false);
            }
        }

        private void ShowRegistrationPanel()
        {
            m_DialogService.Open<RegistrationDialog>();
        }

        private void OnMultiplayerClick()
        {
            if (!m_ServerSigner.IsLogedIn)
            {
                var confirmationDialog = m_DialogService.Open<ConfirmationDialog>();

                confirmationDialog.SetInfo(GetConfirmationInfo(ConfirmationDialogType.LogIn), (isConfirmed) =>
                {
                    m_DialogService.Close<ConfirmationDialog>();
                    if (isConfirmed)
                    {
                        ShowAuthorizationPanel();
                    }
                });
            }
            else
            {
                // Show Multiplayer screen
            }
        }

        private void OnAuthorizationCancel()
        {
            m_DialogService.Close<AuthorizationDialog>();
        }

        private void OnRegistrationCancel()
        {
            m_DialogService.Close<RegistrationDialog>();
        }

        private void OnAuthorizationClick()
        {
            m_ServerSigner.TryToSignIn(m_AuthorizationDialog.Login, m_AuthorizationDialog.Password, (success, message) =>
            {
                if (success)
                {
                    m_DialogService.Close<AuthorizationDialog>();
                    RemoveSignListeners();
                }
                else
                {
                    m_AuthorizationDialog.Message = message;
                }
            });
        }

        private void Registration()
        {
            m_ServerSigner.TryToSignUp(m_RegistrationDialog.GetSignUpModel());
        }

        private void RemoveSignListeners()
        {
            RemoveAuthorizationListeners();
            RemoveRegistrationListeners();
        }

        private void AddMainMenuListeners()
        {
            RemoveMainMenuListeners();
            m_MainScreen.MultiplayerBtn.onClick.AddListener(OnMultiplayerClick);
        }

        private void RemoveMainMenuListeners()
        {
            m_MainScreen.MultiplayerBtn.onClick.RemoveListener(OnMultiplayerClick);
        }

        private void AddAuthorizationListeners()
        {
            RemoveAuthorizationListeners();
            m_AuthorizationDialog.OfflineBtn.onClick.AddListener(OnAuthorizationCancel);
            m_AuthorizationDialog.AuthorizationBtn.onClick.AddListener(OnAuthorizationClick);
            m_AuthorizationDialog.TransitToRegistrationBtn.onClick.AddListener(ShowRegistrationPanel);
        }

        private void RemoveAuthorizationListeners()
        {
            m_AuthorizationDialog.OfflineBtn.onClick.RemoveListener(OnAuthorizationCancel);
            m_AuthorizationDialog.AuthorizationBtn.onClick.RemoveListener(OnAuthorizationClick);
            m_AuthorizationDialog.TransitToRegistrationBtn.onClick.RemoveListener(ShowRegistrationPanel);
        }

        private void AddRegistrationListeners()
        {
            RemoveRegistrationListeners();
            m_RegistrationDialog.RegistrationBtn.onClick.AddListener(Registration);
            m_RegistrationDialog.CancelRegistration.onClick.AddListener(OnRegistrationCancel);
        }

        private void RemoveRegistrationListeners()
        {
            m_RegistrationDialog.RegistrationBtn.onClick.RemoveListener(Registration);
            m_RegistrationDialog.CancelRegistration.onClick.RemoveListener(OnRegistrationCancel);
        }

        private ConfirmationDialogInfo GetConfirmationInfo(ConfirmationDialogType confirmationPopupType)
        {
            //TODO:Loading localization by key and launguage tag

            return new ConfirmationDialogInfo()
            {
                Title = "��� �� �� �����",
                Message = "��������, �� ��� �� �����?",
                ConfirmText = "������!",
                CancelText = "� ���������!",
            };
        }
    }
}