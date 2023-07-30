using Common;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class NavigationPanelConroller : MonoBehaviour
    {
        public NavigationMainMenuViewModel NavigationMainMenuViewModel => m_NavigationMainMenuViewModel;
        public SignInViewModel SignInViewModel => m_SignInViewModel;
        public SignUpViewModel SignUpViewModel => m_SignUpViewModel;

        [Header("ViewModels")]
        [SerializeField]
        private NavigationMainMenuViewModel m_NavigationMainMenuViewModel;

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

            SignInViewModel.Init();
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
            m_NavigationMainMenuViewModel.gameObject.SetActive(false);

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
                    m_NavigationMainMenuViewModel.gameObject.SetActive(true);
                    break;
            }
        }

        public void SetLogInFailed(string message)
        {
            m_SignInViewModel.Message = message;
        }

        private void MoveToPosition(Vector3 position, bool isImmediately)
        {
            transform.DOLocalMove(position, isImmediately ? 0 : m_MoveDuration);
        }
    }
}