using UnityEngine;

namespace Common
{
    public class GameCloser : MonoBehaviour
    {
        public void Close()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
              Application.Quit();
            #endif
        }
    }
}