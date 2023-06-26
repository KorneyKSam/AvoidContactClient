using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class NavigationPanel : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private SignPanel m_SignPanel; 

        [Header("Settings")]
        [SerializeField]
        private float m_Duration = 2f;

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
        }

        public void MoveToMiddle(bool isImmediately = false)
        {
            MoveToPosition(m_MiddlePosition, isImmediately);
        }

        public void MoveToLeft(bool isImmediately = false)
        {
            MoveToPosition(m_LeftPosition, isImmediately);
        }

        private void MoveToPosition(Vector3 position, bool isImmediately)
        {
            transform.DOLocalMove(position, isImmediately ? 0 : m_Duration);
        }
    }
}