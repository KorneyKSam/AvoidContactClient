using System;
using UnityEngine;
using UnityEngine.UI;

namespace Decorations
{
    public class TableLamp : MonoBehaviour
    {
        public event Action OnLightSwitch;
        public bool IsLightOn 
        { 
            get => m_IsLightOn;
            private set
            {
                m_IsLightOn = value;
                OnLightSwitch?.Invoke();
            } 
        }

        [SerializeField]
        private Button m_SwitchButton;

        private bool m_IsLightOn;

        private void Start()
        {
            m_SwitchButton.onClick.AddListener(SwitchLight);
        }

        private void OnDestroy()
        {
            m_SwitchButton.onClick.RemoveAllListeners();
        }

        private void SwitchLight()
        {
            IsLightOn = !IsLightOn;
        }
    }
}