using Common;
using Common.Data;
using DialogBoxService;
using Networking;
using SceneLoading;
using UI.DialogBoxes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenuController : MonoBehaviour, IInitializable
    {
        private const int FakeLoadingMilliseconds = 2000;

        [Header("Buttons")]
        [SerializeField]
        private DeskUI m_DeskUI;

        [SerializeField]
        private Button m_DeveloperBtn;

        [SerializeField]
        private Button m_LiqudatorNotes;

        [SerializeField]
        private Button m_PersonalFileBtn;

        [SerializeField]
        private Button m_ExitBtn;

        [Inject]
        private ServerConnector m_ServerConnector;

        [Inject]
        private SignService m_AuthorizationService;

        [Inject]
        private DialogService m_DialogService;

        [Inject]
        private FakeLoader m_FakeLoader;

        [Inject]
        private DataService m_DataService;

        public void Initialize()
        {
            m_FakeLoader.ShowFakeLoadAnimation(FakeLoadingMilliseconds, allowActivation: false, useResumeButton: false);
            TryToConnectToServer();
            AddMainMenuListeners();
        }

        private void TryToConnectToServer()
        {
            if (!m_ServerConnector.IsConnected)
            {
                m_ServerConnector.Connect((isConnected) =>
                {
                    m_FakeLoader.AllowActivation(true);
                    if (isConnected)
                    {
                        TryToAuthorizate();
                    }
                });
            }
            else
            {
                m_FakeLoader.AllowActivation(true);
            }
        }

        private void TryToAuthorizate()
        {
            var authorizationData = m_DataService.Load<AuthorizationData>();

            if (authorizationData.IsAutomaticAuthorization)
            {
                m_AuthorizationService.TryToSignIn(authorizationData.Login, authorizationData.Password,
                (success) =>
                {
                    if (!success)
                    {
                        authorizationData.IsAutomaticAuthorization = false;
                        m_DataService.Save(authorizationData);
                    }
                });
            }
        }

        private void AddMainMenuListeners()
        {
            RemoveMainMenuListeners();
            m_DeskUI.MultiplayerBtn.onClick.AddListener(OnMultiplayerClick);
        }

        private void RemoveMainMenuListeners()
        {
            m_DeskUI.MultiplayerBtn.onClick.RemoveListener(OnMultiplayerClick);
        }

        private void OnMultiplayerClick()
        {
            if (!m_AuthorizationService.IsLogedIn)
            {
                var confirmationDialog = m_DialogService.Open<ConfirmationDialog>();
                confirmationDialog.SetInfo(GetConfirmationInfo(ConfirmationDialogType.LogIn), (isConfirmed) =>
                {
                    if (isConfirmed)
                    {
                        ShowAuthorizationDialog();
                    }
                    else
                    {
                        m_DialogService.Close<ConfirmationDialog>();
                    }
                });
            }
            else
            {
                // Show Multiplayer screen
            }
        }


        private void ShowAuthorizationDialog()
        {
            var authrizationDialog = m_DialogService.Open<AuthorizationDialog>();
            AddAuthorizationListeners(authrizationDialog);
        }

        private void HideAuthorizationPanel()
        {
            var authrizationDialog = m_DialogService.Close<AuthorizationDialog>();
            RemoveAuthorizationListeners(authrizationDialog);
        }

        private void AddAuthorizationListeners(AuthorizationDialog authorizationDialog)
        {
            RemoveAuthorizationListeners(authorizationDialog);
            authorizationDialog.OnCancelClick += HideAuthorizationPanel;
            authorizationDialog.OnRegistrationClick += ShowRegistrationPanel;
        }

        private void RemoveAuthorizationListeners(AuthorizationDialog authorizationDialog)
        {
            authorizationDialog.OnCancelClick -= HideAuthorizationPanel;
            authorizationDialog.OnRegistrationClick -= ShowRegistrationPanel;
        }

        private void ShowRegistrationPanel()
        {
            m_DialogService.Open<RegistrationDialog>();
        }

        private void OnRegistrationCancel()
        {
            m_DialogService.Close<RegistrationDialog>();
        }

        private ConfirmationDialogInfo GetConfirmationInfo(ConfirmationDialogType confirmationPopupType)
        {
            //TODO:Loading localization by key and launguage tag

            return new ConfirmationDialogInfo()
            {
                Title = "Кто ты по жизни",
                Message = "Ответишь, ты кто по жизни?",
                ConfirmText = "Отвечу!",
                CancelText = "Я меньжуюсь!",
            };
        }
    }
}