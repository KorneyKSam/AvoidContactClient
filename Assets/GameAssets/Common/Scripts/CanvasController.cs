using UnityEngine;
using Zenject;

namespace Common
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasController : MonoBehaviour
    {
        [Inject]
        private CameraController m_CameraController;

        private Canvas m_Canvas;

        [Inject]
        private void Construct(CameraController cameraController)
        {
            m_Canvas = GetComponent<Canvas>();
            m_Canvas.worldCamera = cameraController.Camera;
        }
    }
}