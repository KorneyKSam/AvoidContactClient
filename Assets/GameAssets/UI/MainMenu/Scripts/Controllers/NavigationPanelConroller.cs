using Common;
using DG.Tweening;
using Networking;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NavigationPanelConroller : MonoBehaviour
    {
        public event Action<SignInModel> OnAuthorizationClick;
        public SignInModel SignInModel => m_SignInViewModel.Model;

        [Header("Main panel buttons")]
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

        [Header("Authorization buttons")]
        [SerializeField]
        private Button m_AuthorizationBtn;

        [SerializeField]
        private Button m_OfflineBtn;

        [SerializeField]
        private Button m_TrasitRegistrationBtn;

        [SerializeField]
        private Button m_ResetPasswordBtn;

        [Header("Registration buttons")]
        [SerializeField]
        private Button m_RegistrationBtn;

        [SerializeField]
        private Button m_CancelRegistration;

        [Header("References")]
        [SerializeField]
        private GameObject m_ManMenuPanel;

        [SerializeField]
        private SignInViewModel m_SignInViewModel;

        [SerializeField]
        private SignUpViewModel m_SignUpViewModel;

        [Header("Settings")]
        [SerializeField]
        private float m_MoveDuration = 0.5f;

        private CameraController m_CameraController;
        private Vector3 m_LeftPosition;
        private Vector3 m_MiddlePosition;

        public void Init(CameraController cameraController)
        {
            m_CameraController = cameraController;

            m_LeftPosition = transform.localPosition;

            m_MiddlePosition = new Vector3(m_CameraController.GetCameraScreenSize().x / 2,
                                           transform.localPosition.y,
                                           transform.localPosition.z);
            AddAuthorizationListeners();
        }

        public void MoveToMiddle(bool isImmediately = false)
        {
            MoveToPosition(m_MiddlePosition, isImmediately);
        }

        public void MoveToLeft(bool isImmediately = false)
        {
            MoveToPosition(m_LeftPosition, isImmediately);
        }

        public void SetNavigationContent(NavigationPanelContent navigationPanelContent)
        {
            m_SignInViewModel.gameObject.SetActive(false);
            m_SignUpViewModel.gameObject.SetActive(false);
            m_ManMenuPanel.SetActive(false);

            switch (navigationPanelContent)
            {
                case NavigationPanelContent.Authorization:
                    m_SignInViewModel.gameObject.SetActive(true);
                    break;
                case NavigationPanelContent.Registraction:
                    m_SignUpViewModel.gameObject.SetActive(true);
                    break;
                default:
                case NavigationPanelContent.MainContent:
                    m_ManMenuPanel.SetActive(true);
                    break;
            }
        }

        public void UpdateSignInView(bool isAutomaticAuthorization, string message = "")
        {
            m_SignInViewModel.Message = message;
            m_SignInViewModel.IsAutomaticAuthorization = isAutomaticAuthorization;
        }

        public void SetLogInFailed(string message)
        {
            m_AuthorizationBtn.interactable = true;
            m_SignInViewModel.Message = message;
        }

        private void SendLogIn()
        {
            //To do validation
            m_AuthorizationBtn.interactable = false;
            OnAuthorizationClick?.Invoke(m_SignInViewModel.Model);
        }


        private void AddAuthorizationListeners()
        {
            RemoveAuthorizationListeners();
            m_AuthorizationBtn.onClick.AddListener(SendLogIn);
        }

        private void RemoveAuthorizationListeners()
        {
            m_AuthorizationBtn.onClick.RemoveListener(SendLogIn);
        }

        private void MoveToPosition(Vector3 position, bool isImmediately)
        {
            transform.DOLocalMove(position, isImmediately ? 0 : m_MoveDuration);
        }
    }
}