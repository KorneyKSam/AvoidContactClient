using DG.Tweening;
using UnityEngine;

namespace Common
{
    public class CameraController : MonoBehaviour
    {
        public Camera Camera => m_Camera;
        public Vector2 CameraPosition => m_Camera.transform.position;

        private const float Duration = 2f;

        [SerializeField]
        private Camera m_Camera;

        public void MoveCamera(Vector2 position, bool moveImmediately = false)
        {
            m_Camera.transform.DOLocalMove(position, moveImmediately ? 0 : Duration);
        }

        public Vector2 GetCameraScreenSize()
        {
            return m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        }
    } 
}
