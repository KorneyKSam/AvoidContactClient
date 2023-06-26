using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Vector2 CameraPosition => m_Camera.transform.position;

    private const float Duration = 2f;
    private Camera m_Camera;

    public void MoveCamera(Vector2 position, bool moveImmediately = false)
    {
        m_Camera.transform.DOLocalMove(position, moveImmediately ? 0 : Duration);
    }

    public Vector2 GetCameraScreenSize()
    {
        return m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
    }

    private void Awake()
    {
        m_Camera ??= GetComponent<Camera>();
    }
}
