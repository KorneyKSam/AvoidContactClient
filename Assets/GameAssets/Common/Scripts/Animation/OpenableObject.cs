using UnityEngine;
using UnityEngine.UI;

namespace Common.Animation
{
    [RequireComponent(typeof(Button))]
    public class OpenableObject : MonoBehaviour
    {
        public bool IsClosed
        {
            get => m_IsClosed;
            set
            {
                m_IsClosed = value;
                m_AnimatedSprite.Play(m_IsClosed ? CloseAnimation : OpenAnimation);
            }
        }

        [SerializeField]
        private AnimatedSprite m_AnimatedSprite;

        [SerializeField]
        private Button m_Button;

        private const string OpenAnimation = "Open";
        private const string CloseAnimation = "Close";

        private bool m_IsClosed = true;

        private void OnEnable()
        {
            m_Button.interactable = true;
            m_AnimatedSprite.OnAnimationComplete += OnCompleteAnimation;
            m_Button.onClick.AddListener(OnPressButton);
        }

        private void OnDisable()
        {
            m_AnimatedSprite.OnAnimationComplete -= OnCompleteAnimation;
            m_Button.onClick.RemoveListener(OnPressButton);
        }

        private void OnValidate()
        {
            m_Button ??= GetComponent<Button>();
        }

        private void OnPressButton()
        {
            m_Button.interactable = false;
            IsClosed = !IsClosed;
        }

        private void OnCompleteAnimation()
        {
            m_Button.interactable = true;
        }
    }
}