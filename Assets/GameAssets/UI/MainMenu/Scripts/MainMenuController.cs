using AdvancedDebugger;
using AvoidContactCommon.Validation;
using Common;
using Common.Animation;
using Decorations;
using DialogBoxService;
using Networking;
using Networking.Sign;
using Networking.Sign.Data;
using NPCs.Sporozoa;
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

        [Header("Decorations")]
        [SerializeField]
        private AnimatedSprite m_AnimatedSporozoa;

        [SerializeField]
        private TableLamp m_TableLamp;

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

        private SporozoaInFlaskBehaviour m_SporozoaInFlaskBehaviour;
        private PersonalFileDialog m_PersonalFileDialog;

        public void Initialize()
        {
            m_FakeLoader.ShowFakeLoadAnimation(FakeLoadingMilliseconds, allowActivation: false, useResumeButton: false);
            TryToConnectToServer();
            AddMainMenuListeners();
            m_SporozoaInFlaskBehaviour = new SporozoaInFlaskBehaviour(m_AnimatedSporozoa, m_TableLamp);
        }

        private void TryToConnectToServer()
        {
            if (!m_ServerConnector.IsConnected)
            {
                m_ServerConnector.OnConnectionResult += TryToAuthorizate;
                m_ServerConnector.Connect();
            }
            else
            {
                m_FakeLoader.AllowActivation(true);
            }
        }

        private void TryToAuthorizate(bool isConnected)
        {
            m_ServerConnector.OnConnectionResult -= TryToAuthorizate;
            m_FakeLoader.AllowActivation(true);

            if (isConnected)
            {
                var authorizationData = m_DataService.Load<AuthorizationData>();
                Debugger.Log($"Automatic authorization: {authorizationData.IsAutomaticAuthorization}", DebuggerLog.InfoDebug);
                if (authorizationData.IsAutomaticAuthorization)
                {
                    m_AuthorizationService.TryToSignIn(authorizationData.Login, authorizationData.Password,
                    (result) =>
                    {
                        if (result != SignInResult.Success && result != SignInResult.AccountIsOccupied)
                        {
                            m_DataService.Remove<AuthorizationData>();
                        }
                    });
                }
                else
                {
                    m_DataService.Remove<AuthorizationData>();
                }
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
            m_PersonalFileDialog = m_DialogService.Open<PersonalFileDialog>(() =>
            {
                m_PersonalFileDialog.MoveSheet(PersonalFileSheet.Registration, FileSide.Left);
            });

            AddPersonalFileListeners();
            m_PersonalFileDialog.ActivateFileSheets(isActive: true, PersonalFileSheet.Registration, PersonalFileSheet.PlayerInfo);
        }

        private void AddPersonalFileListeners()
        {
            m_PersonalFileDialog.OnCancelClick += OnPersonalFileCancel;
        }

        private void OnPersonalFileCancel()
        {
            m_PersonalFileDialog.OnCancelClick -= OnPersonalFileCancel;
            m_DialogService.Close<PersonalFileDialog>().OnCancelClick -= OnPersonalFileCancel;
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