using System;
using UnityEngine;

namespace Common.Animation
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedSprite : MonoBehaviour
    {
        public event Action OnAnimationComplete;
        public bool IsPlaying => m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;

        [SerializeField]
        private Animator m_Animator;

        public void PlayAnimation(string animationName)
        {
            m_Animator.Play(animationName);
        }

        /// <summary>
        /// Animator Event
        /// </summary>
        private void InvokeOnAnimationComplete()
        {
            OnAnimationComplete?.Invoke();
        }

        private void OnValidate()
        {
            m_Animator ??= GetComponent<Animator>();
        }
    }
}